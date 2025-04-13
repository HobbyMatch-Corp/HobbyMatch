
using MudBlazor;

namespace HobbyMatch.App
{
    public class CustomTheme : MudTheme
    {
        public CustomTheme()
        {

            PaletteLight = new PaletteLight()
            {
                Primary = new MudBlazor.Utilities.MudColor("#213d5d"),
                Secondary = new MudBlazor.Utilities.MudColor("#1e3957"),
                Tertiary = new MudBlazor.Utilities.MudColor("#007EA7"),
                Info = new MudBlazor.Utilities.MudColor("#00A8E8")
            };

            PaletteDark = new PaletteDark()
            {
                Primary = Colors.Blue.Lighten1
            };

            LayoutProperties = new LayoutProperties()
            {
                DrawerWidthLeft = "260px",
                DrawerWidthRight = "300px"
            };
        }
    }
}
