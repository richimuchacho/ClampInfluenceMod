using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.CampaignSystem;


namespace InfluenceMod
{
    public class MySubModule : MBSubModuleBase
    {
        // A simple mod that set's any clan's influence that goes above 5K, to 5K.
        // anyone below 5K, would be set to 0. Due to internal clan logic, some will always come above 5k
        // somewhat, but that's because of incredible high influence change on a daily basis.
        // and the UI in clan windows isn't entirely showing correct number.


        public override void OnGameInitializationFinished(Game game)
        {
            base.OnGameInitializationFinished(game);

            // this registers a method to be called on the game's internal DailyTickClanEvent.
            // In our case, it's the OnClampClampInfluence method, which clamps the specific clan's influence to 5k.
            CampaignEvents.DailyTickClanEvent.AddNonSerializedListener(this, new Action<Clan>(this.OnClampClanInfluence));

        }

        public void OnClampClanInfluence(Clan c)
        {
            // check if clan have a kingdom.
            if (c.Kingdom != null)
            {
                if (c.Influence > 5000f)
                {
                    //InformationManager.DisplayMessage(new InformationMessage("Clan " + c.Name + " have " + c.Influence.ToString() + ", setting it to 5k:)"));
                    c.Influence = 5000f;
                }
                else if (c.Influence < 0f)
                {
                    c.Influence = 0f;
                }

            }

        }

    }
}
