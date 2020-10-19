

namespace game_state_enums
{
    public enum CorePage
    {
        Menu,
        Stats,
        Shop,
        Play,
        Customize
    }


    // Enum to know which screen of the game loop they are on 
    public enum PlayPage
    {
        Initial,
        Main,
        Diagnose,
        SymptomList,
        SymptomInfo,
        Reasoning,
        Summary
    }


    // Enum to determine which symptom is being investigated
    public enum Symptom
    {
        General,
        Head,
        Neck,
        Lungs,
        Extremities,
        Abdomen,
        Oxygen,
        Imaging,
        Nothing
    }

    // Enum for customizable item types
    public enum ItemType
    {
        Hat,
        Labcoat,
        Stethescope,
        Mask,
        Shoes,
        Pants
    }
}