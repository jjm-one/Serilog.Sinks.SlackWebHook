namespace Serilog.Sinks.SlackWebHook
{
    /// <summary>
    /// Provides a switch to set a slack sink active or inactive on the fly.
    /// </summary>
    public class SlackSinkActivationSwitch
    {
        #region public enum

        /// <summary>
        /// Enum to represent an activation status.
        /// </summary>
        public enum ActivationStatus
        {
            InActive = 0,
            Active = 1
        }

        #endregion

        #region SlackSinkActivationSwitch implementation

        /// <summary>
        /// The current status.
        /// </summary>
        public ActivationStatus Status { get; set; }

        /// <summary>
        /// Initializes new instance of <see cref="SlackSinkActivationSwitch"/>.
        /// </summary>
        /// <param name="activationStatus">The <see cref="ActivationStatus"/>.</param>
        public SlackSinkActivationSwitch(ActivationStatus activationStatus = ActivationStatus.Active)
        {
            Status = activationStatus;
        }

        #endregion
    }
}
