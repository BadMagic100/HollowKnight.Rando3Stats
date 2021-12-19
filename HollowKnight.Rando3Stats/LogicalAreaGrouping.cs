using HollowKnight.Rando3Stats.Util;
using Modding;
using System.Collections.Generic;

namespace HollowKnight.Rando3Stats
{
    public static class LogicalAreaGrouping
    {
        private static SimpleLogger log = new("RandoStats:LogicalAreaGrouping");

        public const string AREA_NAME_CLIFFS = "Howling Cliffs";
        public const string AREA_NAME_CROSSROADS = "Crossroads";
        public const string AREA_NAME_GREENPATH = "Greenpath";
        public const string AREA_NAME_FUNGAL = "Fungal Wastes";
        public const string AREA_NAME_CITY = "City";
        public const string AREA_NAME_WATERWAYS = "Waterways";
        public const string AREA_NAME_PEAKS = "Crystal Peak";
        public const string AREA_NAME_EDGE = "Kingdom's Edge";
        public const string AREA_NAME_DEEPNEST = "Deepnest";
        public const string AREA_NAME_BASIN = "Ancient Basin";
        public const string AREA_NAME_CANYON = "Fog Canyon";
        public const string AREA_NAME_GROUNDS = "Resting Grounds";
        public const string AREA_NAME_PALACE = "White Palace";
        public const string AREA_NAME_GARDENS = "Queen's Gardens";

        private static Dictionary<string, string> areaLookup = new()
        {
            // cliffs/dirtmouth
            { "Howling_Cliffs", AREA_NAME_CLIFFS },
            { "Kings_Pass", AREA_NAME_CLIFFS },
            { "Dirtmouth", AREA_NAME_CLIFFS },
            { "Stag_Nest", AREA_NAME_CLIFFS },
            // crossroads
            { "Forgotten_Crossroads", AREA_NAME_CROSSROADS },
            { "Black_Egg_Temple", AREA_NAME_CROSSROADS },
            { "Ancestral_Mound", AREA_NAME_CROSSROADS },
            // greenpath
            { "Greenpath", AREA_NAME_GREENPATH },
            { "Lake_of_Unn", AREA_NAME_GREENPATH },
            { "Stone_Sanctuary", AREA_NAME_GREENPATH },
            // fungal wastes
            { "Fungal_Core", AREA_NAME_FUNGAL },
            { "Fungal_Wastes", AREA_NAME_FUNGAL },
            { "Mantis_Village", AREA_NAME_FUNGAL },
            { "Queens_Station", AREA_NAME_FUNGAL },
            // city of tears
            { "City_of_Tears", AREA_NAME_CITY },
            { "Kings_Station", AREA_NAME_CITY },
            { "Soul_Sanctum", AREA_NAME_CITY },
            { "Pleasure_House", AREA_NAME_CITY },
            // waterways
            { "Royal_Waterways", AREA_NAME_WATERWAYS },
            { "Ismas_Grove", AREA_NAME_WATERWAYS },
            { "Junk_Pit", AREA_NAME_WATERWAYS },
            // peaks
            { "Hallownests_Crown", AREA_NAME_PEAKS },
            { "Crystal_Peak", AREA_NAME_PEAKS },
            { "Crystallized_Mound", AREA_NAME_PEAKS },
            // edge
            { "Kingdoms_Edge", AREA_NAME_EDGE },
            { "Cast_Off_Shell", AREA_NAME_EDGE },
            { "Tower_of_Love", AREA_NAME_EDGE },
            { "Colosseum", AREA_NAME_EDGE },
            { "Hive", AREA_NAME_EDGE },
            // deepnest
            { "Beasts_Den", AREA_NAME_DEEPNEST },
            { "Weavers_Den", AREA_NAME_DEEPNEST },
            { "Deepnest", AREA_NAME_DEEPNEST },
            { "Failed_Tramway", AREA_NAME_DEEPNEST },
            { "Distant_Village", AREA_NAME_DEEPNEST },
            // basin
            { "Ancient_Basin", AREA_NAME_BASIN },
            { "Abyss", AREA_NAME_BASIN },
            // canyon
            { "Fog_Canyon", AREA_NAME_CANYON },
            { "Teachers_Archives", AREA_NAME_CANYON },
            { "Overgrown_Mound", AREA_NAME_CANYON },
            // grounds
            { "Resting_Grounds", AREA_NAME_GROUNDS },
            { "Blue_Lake", AREA_NAME_GROUNDS },
            { "Spirits_Glade", AREA_NAME_GROUNDS },
            // palace
            { "Palace_Grounds", AREA_NAME_PALACE },
            { "White_Palace", AREA_NAME_PALACE },
            // garden
            { "Queens_Gardens", AREA_NAME_GARDENS }
        };

        private static Dictionary<string, string> transitionLookup = new()
        {
            // dirtmouth rooms
            { "Room_Bretta[right1]", AREA_NAME_CLIFFS },
            { "Room_mapper[left1]", AREA_NAME_CLIFFS },
            { "Room_shop[left1]", AREA_NAME_CLIFFS },
            { "Room_Town_Stag_Station[left1]", AREA_NAME_CLIFFS },
            { "Room_Ouiji[left1]", AREA_NAME_CLIFFS },
            { "Grimm_Divine[left1]", AREA_NAME_CLIFFS },
            { "Grimm_Main_Tent[left1]", AREA_NAME_CLIFFS },
            // crossroads rooms
            { "Room_Mender_House[left1]", AREA_NAME_CROSSROADS },
            { "Room_Charm_Shop[left1]", AREA_NAME_CROSSROADS },
            { "Room_ruinhouse[left1]", AREA_NAME_CROSSROADS }, // sly room
            // city rooms
            { "Room_nailsmith[left1]", AREA_NAME_CITY },
            { "Ruins_House_03[left1]", AREA_NAME_CITY }, // emilitia top door
            { "Ruins_House_03[left2]", AREA_NAME_WATERWAYS }, // emilitia bottom door
            // spirit's glade
            // this doesn't have an area name defined in room rando but it gets one in area rando... so we need to handle it in both lookup tables
            { "RestingGrounds_08[left1]", AREA_NAME_GROUNDS }, 
            // gardens room
            { "Room_Queen[left1]", AREA_NAME_GARDENS }
        };

        public static string GetLogicalAreaOf(string transition)
        {
            TransitionDef def = TransitionReflection.GetTransitionDef(transition);
            if (def.areaName == null || def.areaName == "")
            {
                return transitionLookup[transition];
            }
            else
            {
                return areaLookup[def.areaName];
            }
        }
    }
}
