using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Go.To.Sleep
{
    /// <summary>
    /// The view model to manage the application
    /// </summary>
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        #region Implementation of INotifyPropertyChanged

        /// <summary>
        /// The event that is fired when any child property changes its value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Calling this to fire a <see cref="PropertyChanged"/> event
        /// </summary>
        /// <param name="name"></param>
        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region Private Fields

        private double _angle;
        private bool _idle;
        private System.Windows.Threading.DispatcherTimer _timer = new System.Windows.Threading.DispatcherTimer();

        #endregion

        #region Public Properties

        /// <summary>
        /// The angle of the handle
        /// </summary>
        public double Angle
        {
            get { return _angle; }
            set
            {
                // Making sure the angle is between 0 and 360
                value = value % 360;
                if (value < 0)
                    value = 360 + value;

                // Assigning the value
                _angle = value;

                OnPropertyChanged(nameof(Angle));
                OnPropertyChanged(nameof(Minutes));
                OnPropertyChanged(nameof(Seconds));
            }
        }

        /// <summary>
        /// The remaining minutes to the sleep
        /// </summary>
        public string Minutes => ((int)(Angle / 6)).ToString("D2");

        /// <summary>
        /// The remaining seconds (just the seconds of the last minute) to the sleep
        /// </summary>
        public string Seconds => ((int)(((Angle / 6) - (int)(Angle / 6))*60) ).ToString("D2");

        /// <summary>
        /// The annotation around the handle
        /// </summary>
        public ObservableCollection<Annotation> Annotations { get; set; }

        /// <summary>
        /// If the program is idle or is it currently awaiting sleep
        /// </summary>
        public bool Idle
        {
            get => _idle;
            set
            {
                _idle = value;
                OnPropertyChanged(nameof(ButtonText));
                OnPropertyChanged(nameof(Idle));
            }
        }

        /// <summary>
        /// The text to be shown on the button
        /// </summary>
        public string ButtonText { get => !Idle ? "Stop Timer" : "Sleep"; }

        #endregion

        #region Commands

        /// <summary>
        /// The command that house the sleep method
        /// </summary>
        public ICommand SleepCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ApplicationViewModel()
        {
            // Assigning the sleep method
            SleepCommand = new ReleyCommand(WaitThenSleep, () => true);

            // Initiating the annotation
            Annotations = new ObservableCollection<Annotation>();

            // Giving values to the annotation, a minute for every 6 degrees 
            for (int i = 0; i < 360; i += 30)
            {
                Annotations.Add(new Annotation() { Angle = i, Text = (i / 6).ToString() });
            }

            // Initiating the timer
            _timer.Tick += dispatcherTimer_Tick;
            _timer.Interval = new TimeSpan(0, 0, 1);

            // Initiating the idle state
            Idle = true;
        }

        #endregion

        #region Events

        /// <summary>
        /// Event to hundle the ticking of the time, removing 1/10 degree for every second ticking
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // Reduce the angle in seperate variable as the Angle property is limited to be from 0 to 360
            var newAngle = Angle - 1.0 / 10;

            // If the angle reached less than zero, then call the sleep method
            if (newAngle <= 0)
            {
                WaitThenSleep();

                Task.Run(Sleep);
            }

            // Re-assing the angle
            Angle = newAngle;
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Sleep method, will start the timer if the idle is true and will stop the timer if the idle is false
        /// </summary>
        private void WaitThenSleep()
        {
            if (!Idle)
            {
                Idle = true;

                _timer.Stop();
            }
            else
            {
                Idle = false;

                _timer.Start();
            }
        } 

        #endregion

        #region Helping Methods

        /// <summary>
        /// Force the pc to go to sleep
        /// </summary>
        private void Sleep()
        {
            System.Windows.Forms.Application.SetSuspendState(System.Windows.Forms.PowerState.Suspend, true, true);
        } 

        #endregion
    }

}
