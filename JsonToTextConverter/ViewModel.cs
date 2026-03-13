using System.ComponentModel;

namespace JsonToTextConverter
{
    public class ViewModel() : INotifyPropertyChanged
    {
        /// <summary>
        /// The relative or absolute path to the JSON file
        /// </summary>
        public string InputJson
        {
            get
            {
                return _inputJson;
            }
            set
            {
                _inputJson = value;
                OnPropertyChanged(nameof(InputJson));
            }
        }

        // NEVER TOUCH THIS! use the property InputJson instead
        private string _inputJson = string.Empty;

        ///// <summary>
        ///// The relative or absolute path to the desired output directory
        ///// </summary>
        //public string OutputDirectory
        //{
        //    get
        //    {
        //        return _outputDirectory;
        //    }
        //    set
        //    {
        //        _outputDirectory = value;
        //        OnPropertyChanged(nameof(OutputDirectory));
        //    }
        //}

        //// NEVER TOUCH THIS! use the property OutputDirectory instead 
        //private string _outputDirectory = string.Empty;

        /// <summary>
        /// If the person keeps pressing the button and accomplishing nothing, we'll launch a help message
        /// </summary>
        public string FlounderingHelpMessage
        {
            get
            {
                return _unsuccessfulButtonPresses > 1 ? "Stuck? Double check that your input path is valid JSON." :
                    string.Empty;
            }
        }

        private int _unsuccessfulButtonPresses = 0;

        /// <summary>
        /// When set to "true", one alter will be represented with multiple files
        /// </summary>
        public bool MultiFileAlters
        {
            get
            {
                return _multiFileAlters;
            }
            set
            {
                _multiFileAlters = value;
                OnPropertyChanged(nameof(MultiFileAlters));
            }
        }

        // NEVER TOUCH THIS! Use the property MultiFileAlters instead
        private bool _multiFileAlters = false;

        // This is for the view
        public event PropertyChangedEventHandler? PropertyChanged;

        public void Run()
        {
            // Input validation
            if (!File.Exists(InputJson) || !".JSON".Equals(Path.GetExtension(InputJson), StringComparison.InvariantCultureIgnoreCase))
            {
                // this logic is for the view
                _unsuccessfulButtonPresses++;
                OnPropertyChanged(nameof(FlounderingHelpMessage));

                // if I cared more I'd make this better but I dont
                return;
            }

            // !!JSON PARSING HERE!!

            // this logic is for the view
            _unsuccessfulButtonPresses = 0;
            OnPropertyChanged(nameof(FlounderingHelpMessage));
        }

        /// <summary>
        /// Ignore me!
        /// </summary>
        /// <param name="propertyName">name of changed property</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
