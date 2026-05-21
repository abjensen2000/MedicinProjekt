using System.Net.Http.Json;
using DTO;

namespace LægehusUI
{
    public partial class MainPage : ContentPage
    {
        private HttpClient _client = new HttpClient();
        private LægehusDTO? _currentLægehus;
        private ReceptDTO? _valgtRecept;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void LoginKnap_Clicked(object sender, EventArgs e)
        {
            List<LægehusDTO>? alleLægehuse = await _client.GetFromJsonAsync<List<LægehusDTO>>("http://localhost:5062/api/lægehus/lægehuse");
            if (alleLægehuse != null)
            {
                _currentLægehus = alleLægehuse.Find((i) => i.Ydernummer.Equals(YdernummerEntry.Text, StringComparison.OrdinalIgnoreCase));
                if (_currentLægehus != null)
                {
                    ReceptSection.IsVisible = true;
                    LoginSection.IsVisible = false;
                }
            }
            await VisReceptMenu();
        }

        private async Task VisReceptMenu()
        {
            CprSøgEntry.Text = "";
            PatientReceptListe.IsVisible = false;
            ReceptDetaljerSection.IsVisible = false;
            OpretReceptSection.IsVisible = false;
        }

        private async void SøgPatientKnap_Clicked(object sender, EventArgs e)
        {
            string cpr = CprSøgEntry.Text;
            if (string.IsNullOrWhiteSpace(cpr))
            {
                await DisplayAlert("Fejl", "Indtast et CPR-nummer.", "OK");
                return;
            }

            List<ReceptDTO>? patientsRecepter = await _client.GetFromJsonAsync<List<ReceptDTO>>($"http://localhost:5062/api/lægehus/recepter/patient/{cpr}");

            if (patientsRecepter != null)
            {
                ReceptListeView.Children.Clear();

                if (patientsRecepter.Count == 0)
                {
                    await DisplayAlert("Info", "Patienten har ingen recepter. Du kan oprette en ny nedenfor.", "OK");
                    PatientReceptListe.IsVisible = true; // Vis sektionen så man kan klikke "Opret ny recept"
                    return;
                }

                foreach (var recept in patientsRecepter)
                {
                    var knap = new Button
                    {
                        Text = $"Recept ID: {recept.Id} (Lukket: {recept.Lukket})",
                        BackgroundColor = recept.Lukket ? Colors.Gray : Colors.LightBlue,
                        TextColor = Colors.Black,
                        CornerRadius = 5
                    };

                    // Man kan klikke på alle recepter for at se detaljer/ordinationer
                    knap.Clicked += (s, args) => ReceptValgt(recept);

                    ReceptListeView.Children.Add(knap);
                }

                PatientReceptListe.IsVisible = true;
                ReceptDetaljerSection.IsVisible = false;
                OpretReceptSection.IsVisible = false;
            }
        }

        private async void ReceptValgt(ReceptDTO recept)
        {
            _valgtRecept = recept;
            ValgtReceptInfoLabel.Text = $"Valgt recept: {recept.Id} til CPR: {recept.Cpr}";

            // Ryd Opret ordination felter
            LægemiddelEntry.Text = "";
            DosisEntry.Text = "";
            AntalUdleveringerEntry.Text = "";
            OrdinationerListeView.Children.Clear();

            // Skjul de andre sektioner
            SøgPatientSection.IsVisible = false;
            PatientReceptListe.IsVisible = false;
            ReceptDetaljerSection.IsVisible = true;

            // Hent og vis ordinationer for denne specifikke recept
            try
            {
                // RET EVENTUELT URL'EN så den matcher dit præcise API-endpoint for at hente ordinationer pr. recept
                List<OrdinationDTO>? ordinationer = await _client.GetFromJsonAsync<List<OrdinationDTO>>($"http://localhost:5062/api/lægehus/ordinationer/recept/{recept.Id}");

                if (ordinationer != null && ordinationer.Count > 0)
                {
                    foreach (var ord in ordinationer)
                    {
                        var ordLabel = new Label
                        {
                            Text = $"• {ord.Lægemiddel} - Dosis: {ord.Dosis} (Udl: {ord.AntalUdleveringer})",
                            FontSize = 14,
                            Margin = new Thickness(10, 2)
                        };
                        OrdinationerListeView.Children.Add(ordLabel);
                    }
                }
                else
                {
                    OrdinationerListeView.Children.Add(new Label { Text = "Ingen ordinationer fundet på denne recept.", FontAttributes = FontAttributes.Italic, Margin = new Thickness(10, 0) });
                }
            }
            catch (Exception)
            {
                OrdinationerListeView.Children.Add(new Label { Text = "Kunne ikke hente ordinationer fra serveren.", TextColor = Colors.Red });
            }

            // Hvis recepten er lukket, må man ikke tilføje nye ordinationer
            OpretOrdinationSection.IsVisible = !recept.Lukket;
        }

        private void TilbageFraDetaljerKnap_Clicked(object sender, EventArgs e)
        {
            ReceptDetaljerSection.IsVisible = false;
            SøgPatientSection.IsVisible = true;
            PatientReceptListe.IsVisible = true;
            _valgtRecept = null;
        }

        private async void OpretOrdinationKnap_Clicked(object sender, EventArgs e)
        {
            if (_valgtRecept == null) return;

            if (string.IsNullOrWhiteSpace(LægemiddelEntry.Text) ||
                string.IsNullOrWhiteSpace(DosisEntry.Text) ||
                !int.TryParse(AntalUdleveringerEntry.Text, out int antalUdleveringer))
            {
                await DisplayAlert("Fejl", "Udfyld alle felter korrekt.", "OK");
                return;
            }

            var nyOrdination = new OrdinationDTO(LægemiddelEntry.Text, DosisEntry.Text, antalUdleveringer);

            var response = await _client.PostAsJsonAsync($"http://localhost:5062/api/lægehus/ordinationer/{_valgtRecept.Id}", nyOrdination);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Succes", "Ordination tilføjet!", "OK");

                // Opdater visningen med det samme ved at genindlæse den valgte recept
                ReceptValgt(_valgtRecept);
            }
            else
            {
                await DisplayAlert("Fejl", "Kunne ikke tilføje ordination.", "OK");
            }
        }

        // --- NY LOGIK TIL OPRETTELSE AF RECEPTER ---

        private void ÅbenOpretReceptKnap_Clicked(object sender, EventArgs e)
        {
            string cpr = CprSøgEntry.Text;
            if (string.IsNullOrWhiteSpace(cpr)) return;

            OpretReceptCprLabel.Text = $"Patient CPR: {cpr}";

            SøgPatientSection.IsVisible = false;
            PatientReceptListe.IsVisible = false;
            OpretReceptSection.IsVisible = true;
        }

        private void AnnullerReceptKnap_Clicked(object sender, EventArgs e)
        {
            OpretReceptSection.IsVisible = false;
            SøgPatientSection.IsVisible = true;
            PatientReceptListe.IsVisible = true;
        }

        private async void GemReceptKnap_Clicked(object sender, EventArgs e)
        {
            string cpr = CprSøgEntry.Text;
            if (string.IsNullOrWhiteSpace(cpr)) return;

            // Her opretter vi et nyt ReceptDTO objekt. 
            // Hvis din ReceptDTO constructor kræver andre parametre (fx Id = 0, Lukket = false), så tilpas det her.
            var nyRecept = new ReceptDTO (_currentLægehus.Ydernummer, cpr);

            // RET EVENTUELT URL'EN til dit specifikke endpoint for oprettelse af recepter
            var response = await _client.PostAsJsonAsync("http://localhost:5062/api/lægehus/recepter", nyRecept);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Succes", "Recepten blev oprettet!", "OK");
                OpretReceptSection.IsVisible = false;
                SøgPatientSection.IsVisible = true;

                // Genindlæs listen for at vise den nye recept med det samme
                SøgPatientKnap_Clicked(this, EventArgs.Empty);
            }
            else
            {
                await DisplayAlert("Fejl", "Kunne ikke oprette recepten på serveren.", "OK");
            }
        }
    }
}