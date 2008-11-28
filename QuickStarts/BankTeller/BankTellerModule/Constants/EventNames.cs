namespace BankTellerModule.Constants
{
	public class EventNames
	{
		public const string StatusUpdate = "topic://BankShell/StatusUpdate";
        public const string SkinPopup = "topic://BankShell/SkinPopup";
        public const string SwitchSkin = "topic://BankShell/SwitchSkin";
        public const string CustomerClosed = "topic://BankShell/CustomerClosed";

        // UseRibbonForm
        public const string RibbonSkinChange = "topic://BankShell/RibbonSkinChange";
    
        public const string Exit = "ExitCommandEvent";				// string is repeated in config
        public const string HelpAbout = "HelpAboutCommandEvent";		// string is repeated in config
    }
}