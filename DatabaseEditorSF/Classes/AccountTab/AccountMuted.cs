using System;

namespace DatabaseEditorSF.Classes.AccountTab {
	public class AccountMuted {
		public DateTime muteDate { get; set; }
		public double duration { get; set; }
		public string by { get; set; }
		public string reason { get; set; }

		public DateTime getUnmuteDate() {
			return muteDate.AddMinutes(duration);
		}
	}
}
