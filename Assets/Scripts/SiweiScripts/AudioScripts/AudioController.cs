namespace Siwei
{
    using System.Collections;
    using UnityEngine;
    using Peixi;
    using UniRx;

    public class AudioController : MonoBehaviour
    {

        private Hashtable m_AudioTable; // relationship of audio clip name (key) and audio object (value)
        private Hashtable m_JobTable;   // relationship between audio types (key) and jobs (value)
        private string onPlay = "";

        public AudioSource[] source;
        public AudioObject[] audio;

        public bool debug;

        private enum AudioAction
        {
            START,
            STOP,
            RESTART
        }

        public enum AudioType
        {
            None,
            BGM,     
            SceneInteraction,    
            PlayerInteraction,          
            Dialogue,
            FOOTSTEP

        }

        private void Start()
        {
            AudioEvents.OnAudioStart.Subscribe(x =>
            {
                PlayAudio(AudioRegistration.audioTable[x]);
            });
        }

        [System.Serializable]
        public class AudioObject
        {
            public AudioType type;
            public AudioClip clip;
        }

        private class AudioJob
        {
            public AudioAction action;
            public string clipName;
            public AudioType type;
            public bool fade;
            public WaitForSeconds delay;

            public AudioJob(AudioAction _action, string _clipName, AudioType _type, bool _fade, float _delay)
            {
                action = _action;
                clipName = _clipName;
                type = _type;
                fade = _fade;
                delay = _delay > 0f ? new WaitForSeconds(_delay) : null;
            }
        }

        public void ReceiveSignal(string audioName)
        {
            Debug.Log($"Audio: {audioName}");
        }

        #region Unity Functions
        private void Awake()
        {
            Configure();
        }


        private void OnDisable()
        {
            Dispose();
        }
        #endregion

        #region Public Functions

        public void PlayAudio(string _clipName)
        {
            bool _fade = false;
            float _delay = 0.0f;
            AudioObject _object = (AudioObject)m_AudioTable[_clipName];

            AudioSource _source = GetAudioSource(_object.type);
            if (!_source.isPlaying)
            {
                onPlay = "";
            }
            //Debug.Log("Isplaying:" + _source.isPlaying + " onPlay :" + onPlay);
            if (_source.isPlaying && _clipName == onPlay)
            {
                return;
            }

            AddJob(new AudioJob(AudioAction.START, _clipName, _object.type, _fade, _delay));
            onPlay = _clipName;

        }

        public void StopAudio(string _clipName, bool _fade = false, float _delay = 0.0F)
        {
            AudioObject _object = (AudioObject)m_AudioTable[_clipName];
            AddJob(new AudioJob(AudioAction.STOP, _clipName, _object.type, _fade, _delay));
            onPlay = "";
        }

        public void RestartAudio(string _clipName, bool _fade = false, float _delay = 0.0F)
        {
            AudioObject _object = (AudioObject)m_AudioTable[_clipName];
            AddJob(new AudioJob(AudioAction.RESTART, _clipName, _object.type, _fade, _delay));
        }
        #endregion

        #region Private Functions
        private void Configure()
        {
            m_AudioTable = new Hashtable();
            m_JobTable = new Hashtable();
            GenerateAudioTable();
        }

        private void Dispose()
        {
            // cancel all jobs in progress
            foreach (DictionaryEntry _kvp in m_JobTable)
            {
                IEnumerator _job = (IEnumerator)_kvp.Value;
                StopCoroutine(_job);
            }
        }

        private void AddJob(AudioJob _job)
        {
            // cancel any job that might be using this job's audio source
            RemoveConflictingJobs(_job.type);

            IEnumerator _jobRunner = RunAudioJob(_job);
            StartCoroutine(RunAudioJob(_job));
            m_JobTable.Add(_job.type, _jobRunner);
            Log("Starting job on [" + _job.type + "] with operation: " + _job.action);
        }

        private void RemoveJob(AudioType _type)
        {
            if (m_JobTable.ContainsKey(_type) == null)
            {
                Log("Trying to stop a job [" + _type + "] that is not running.");
                return;
            }
            IEnumerator _runningJob = (IEnumerator)m_JobTable[_type];
            StopCoroutine(_runningJob);
            m_JobTable.Remove(_type);
        }

        private void RemoveConflictingJobs(AudioType _type)
        {
            Log("type is:" + _type);
            //PrintTable(m_JobTable);
            // cancel the job if one exists with the same type
            if (m_JobTable.ContainsKey(_type))
            {
                RemoveJob(_type);
            }
        }

        private IEnumerator RunAudioJob(AudioJob _job)
        {
            if (_job.delay != null) yield return _job.delay;

            AudioSource _source = GetAudioSource(_job.type); // track existence should be verified by now
            AudioObject _object = (AudioObject)m_AudioTable[_job.clipName];
            _source.clip = _object.clip;
            float _initial = 0f;
            float _target = 1f;
            switch (_job.action)
            {
                case AudioAction.START:
                    _source.Play();
                    break;
                case AudioAction.STOP when !_job.fade:
                    _source.Stop();
                    break;
                case AudioAction.STOP:
                    _initial = 1f;
                    _target = 0f;
                    break;
                case AudioAction.RESTART:
                    _source.Stop();
                    _source.Play();
                    break;
            }

            // fade volume
            if (_job.fade)
            {
                float _duration = 1.0f;
                float _timer = 0.0f;

                while (_timer <= _duration)
                {
                    _source.volume = Mathf.Lerp(_initial, _target, _timer / _duration);
                    _timer += Time.deltaTime;
                    yield return null;
                }

                // if _timer was 0.9999 and Time.deltaTime was 0.01 we would not have reached the target
                // make sure the volume is set to the value we want
                _source.volume = _target;

                if (_job.action == AudioAction.STOP)
                {
                    _source.Stop();
                }
            }

            m_JobTable.Remove(_job.type);
            Log("Job count: " + m_JobTable.Count);
        }

        private void GenerateAudioTable()
        {

            foreach (AudioObject _obj in audio)
            {
                // do not duplicate keys
                if (m_AudioTable.ContainsKey(_obj.clip.name))
                {
                    LogWarning("You are trying to register audio [" + _obj.clip.name + "] that has already been registered.");
                }
                else
                {
                    m_AudioTable.Add(_obj.clip.name, _obj);
                    Log("Registering audio [" + _obj.type + "]");
                }
            }
        }

        private AudioSource GetAudioSource(AudioType _type, string _job = "")
        {
            switch (_type) {
                case AudioType.BGM:
                    return source[0];
                case AudioType.SceneInteraction:
                    return source[1];
                case AudioType.PlayerInteraction:
                    return source[2];
                case AudioType.Dialogue:
                    return source[3];
                case AudioType.FOOTSTEP:
                    return source[4];
                default:
                    Log("You are trying to get a [" + _type + "] source");
                    break;
            }
            
            return null;
        }


        private void PrintTable(Hashtable table)
        {
            if (table == null) return;
            foreach (DictionaryEntry _kvp in table)
            {
                Debug.Log(_kvp.Value);
            }

        }

        private void Log(string _msg)
        {
            if (!debug) return;
            Debug.Log("[Audio Controller]: " + _msg);
        }

        private void LogWarning(string _msg)
        {
            if (!debug) return;
            Debug.LogWarning("[Audio Controller]: " + _msg);
        }
        #endregion
    }
}