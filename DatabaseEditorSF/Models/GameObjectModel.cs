using DatabaseEditorSF.Classes.Settings;
using DatabaseEditorSF.Classes.GameObjectTab;

namespace DatabaseEditorSF.Models {
	public class GameObjectModel {
		private GameObjectModel() { gameObject = new GameObject(); }

		private static GameObjectModel model;

		public GameObject gameObject { get; set; }

		public void updateGameObjectFromDatabase(uint id) {
			gameObject = Settings.getWorldDB().getGameObject(id);
		}

		public static GameObjectModel getInstance() {
			if(model == null) { model = new GameObjectModel(); }

			return model;
		}
	}
}
