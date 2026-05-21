using BusinessLayer;
using DataAccess;
using Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<MedicinContext>();
builder.Services.AddScoped<ApotekService>();
builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<OrdinationService>();
builder.Services.AddScoped<ReceptService>();
builder.Services.AddScoped<LægehusService>();
builder.Services.AddScoped<UnitOfWork>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MedicinContext>();

    if (!context.Set<Apotek>().Any())
    {
        var apotek1 = new Apotek("Løve apoteket");
        var apotek2 = new Apotek("Medicinmanden");
        var apotek3 = new Apotek("Apo24/7");

        var lægehus1 = new Lægehus("E7KFT32","Banegårdslægerne");
        var lægehus2 = new Lægehus("AB35CDE","Lægerne i Lunderskov");
        var lægehus3 = new Lægehus("FE67KEG", "Langenæslægen");

        var patient1 = new Patient("123456781111", "Anders");
        var patient2 = new Patient("123412341111", "Peter");
        var patient3 = new Patient("876543219999", "Christina");

        var ordination1 = new Ordination("Morfin", "2 piller dagligt, morgen og aften", 8);
        var ordination2 = new Ordination("Paracetamol", "4 piller dagligt, morgen, middag, eftermiddag, aften", 4);
        var ordination3 = new Ordination("Ibuprofen", "2 piller dagligt, morgen og aften", 6);
        var ordination4 = new Ordination("Melatonin", "1 sprøjt dagligt, aften", 2);
        ordination4.AntalForetagneUdleveringer = 2;

        context.AddRange(ordination1, ordination2, ordination3, ordination4);
        context.SaveChanges();

        var recept1 = new Recept("E7KFT32", "123456781111");
        recept1.OrdinationerId.Add(ordination1.Id);
        var recept2 = new Recept("AB35CDE", "123412341111");
        recept2.OrdinationerId.Add(ordination2.Id);
        recept1.OrdinationerId.Add(ordination3.Id);
        recept1.OrdinationerId.Add(ordination4.Id);


        context.AddRange(apotek1, apotek2, apotek3, lægehus1, lægehus2, lægehus3, patient1, patient2, recept1, recept2);
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
