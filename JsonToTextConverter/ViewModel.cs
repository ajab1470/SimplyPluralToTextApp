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
                PropertyChangedEvent(nameof(InputJson));
            }
        }

        // NEVER TOUCH THIS! use the property InputJson instead
        private string _inputJson;

        /// <summary>
        /// The relative or absolute path to the desired output directory
        /// </summary>
        public string OutputDirectory
        {
            get
            {
                return _outputDirectory;
            }
            set
            {
                _outputDirectory = value;
                PropertyChangedEvent(nameof(OutputDirectory));
            }
        }

        // NEVER TOUCH THIS! use the property OutputDirectory instead 
        private string _outputDirectory = string.Empty;

        /// <summary>
        /// If we should show the Hint tooltip
        /// </summary>
        public bool MoreThanOneUnsuccessfulButtonPress
        {
            get
            {
                return _unsuccessfulButtonPresses > 1;
            }
        }

        private int _unsuccessfulButtonPresses = 0;

        // This is for the view
        public event PropertyChangedEventHandler? PropertyChanged;

        public void Run()
        {
            // Input validation
            if (!File.Exists(InputJson) || !"JSON".Equals(Path.GetExtension(InputJson), StringComparison.InvariantCultureIgnoreCase))
            {
                // this logic is for the gui
                _unsuccessfulButtonPresses++;
                PropertyChangedEvent(nameof(MoreThanOneUnsuccessfulButtonPress));

                // if I cared more I'd make this better but I dont
                return;
            }

            // !!JSON PARSING HERE!!

            // this logic is for the view
            _unsuccessfulButtonPresses = 0;
        }

        /// <summary>
        /// Ignore me!
        /// </summary>
        /// <param name="propertyName">name of changed property</param>
        protected void PropertyChangedEvent(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
