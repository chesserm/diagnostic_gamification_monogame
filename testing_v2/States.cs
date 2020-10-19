

namespace game_state_enums
{
    public enum CoreState
    {
        Menu,
        Stats,
        Shop,
        Play,
        Customize
    }


    // Enum to know which screen of the game loop they are on 
    public enum PlayState
    {
        Initial,
        Main,
        Diagnose,
        SymptomList,
        SymptomInfo,
        Reasoning,
        Summary,
        Back
    }


    // Enum to determine which symptom is being investigated
    public enum SymptomState
    {
        General,
        Head,
        Neck,
        Lungs,
        Extremities,
        Abdomen,
        Oxygen,
        Imaging,
        Bloodwork,
        Skin,
        Nothing,
        MainMenu
    }

    public enum DiagnosisState
    {
        Undiagnosed,
        COPD,
        Pneumonia,
        CHF,
        Back
    }


}