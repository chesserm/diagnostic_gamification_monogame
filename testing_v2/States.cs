

namespace state_helpers
{

    enum CorePage
    {
        Menu,
        Stats,
        Shop,
        Play,
        Customize
    }


    // Enum to know which screen of the game loop they are on 
    enum PlayScreen
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
    enum Symptom
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


}