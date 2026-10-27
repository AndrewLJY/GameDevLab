using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Actions/StopStarmanMusic")]
public class StopStarmanMusicAction : Action
{
    public override void Act(StateController controller)
    {
        ((BuffStateController)controller).StopStarmanMusic();
    }
}