namespace jjm.one.Serilog.Sinks.SlackWebHook
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
        public enum SlackSinkActivationStatus
        {
            InActive = 0,
            Active = 1
        }

        #endregion

        #region SlackSinkActivationSwitch implementation

        /// <summary>
        /// The current status.
        /// </summary>
        public SlackSinkActivationStatus SlackSinkStatus { get; set; }

        /// <summary>
        /// Initializes new instance of <see cref="SlackSinkActivationSwitch"/>.
        /// </summary>
        /// <param name="slackSinkActivationStatus">The <see cref="SlackSinkActivationStatus"/>.</param>
        public SlackSinkActivationSwitch(SlackSinkActivationStatus slackSinkActivationStatus = SlackSinkActivationStatus.Active)
        {
            SlackSinkStatus = slackSinkActivationStatus;
        }

        #endregion
    }
}
