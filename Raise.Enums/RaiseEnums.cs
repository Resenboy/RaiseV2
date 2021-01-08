using System.ComponentModel.DataAnnotations;

namespace Raise.Enums
{
    //
    // Summary:
    //     Represents PanningMode
    public enum PanningMode
    {
        //
        // Summary:
        //     Represents SingleFinger
        SingleFinger = 0,
        //
        // Summary:
        //     Represents TwoFinger
        TwoFinger = 1
    }

    public enum ProfileEnum
    {
        [Display(Description = "Aluno")]
        Student,

        [Display(Description = "Treinador")]
        Trainer,

        [Display(Description = "Academia")]
        Gym,

        [Display(Description = "Matriz")]
        Matrix,

        [Display(Description = "Iniciante")]
        Beginner,

        [Display(Description = "Moderado")]
        Medium,

        [Display(Description = "Profissional")]
        Pro,
    }

    public enum StartPageType
    {
        Login,
        Signup
    }

    public enum ProfileType
    {
        Athlete,
        Tecnic,
        Teacher,
        Academy,
        Gamer
    }

    public enum Games
    {
        [Display(Description = "Sem preferências")]
        NoPreferences,
        [Display(Description = "Vários")]
        MostOne,
        [Display(Description = "PUG")]
        PUG,
        [Display(Description = "Call of Duty")]
        COD,
        [Display(Description = "Counter Strike GO")]
        CSGO,
        [Display(Description = "League of Legends")]
        LOL,
        [Display(Description = "Fortnite")]
        Fortnite,
        [Display(Description = "FIFA")]
        FIFA,
        [Display(Description = "RGP")]
        RPG
    }

    public enum ImageSourceType
    {
        FromFile,
        FromResource,
        FromStream,
        FromURL
    }
}
