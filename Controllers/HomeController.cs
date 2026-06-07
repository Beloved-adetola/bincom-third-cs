using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC_Portfolio.Models;

namespace MVC_Portfolio.Controllers;

public class HomeController(IWebHostEnvironment environment) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult About() => View();
    public IActionResult Skills() => View();
    public IActionResult Projects() => View();
    public IActionResult Contact() => View();

    public IActionResult Experience()
    {
        return View();
    }

    [HttpGet]
    public IActionResult TaxCalculator() => View();

    [HttpPost]
    public IActionResult TaxCalculator(decimal income)
    {
        decimal tax = CalculateTax(income);
        ViewBag.Tax = tax;
        return View();
    }

    private decimal CalculateTax(decimal income)
{
    decimal tax = 0;

    if (income > 300000)
    {
        tax += 300000 * 0.07m;
        income -= 300000;
    }
    else return income * 0.07m;

    if (income > 300000)
    {
        tax += 300000 * 0.11m;
        income -= 300000;
    }
    else return tax + (income * 0.11m);

    if (income > 500000)
    {
        tax += 500000 * 0.15m;
        income -= 500000;
    }
    else return tax + (income * 0.15m);

    if (income > 500000)
    {
        tax += 500000 * 0.19m;
        income -= 500000;
    }
    else return tax + (income * 0.19m);

    if (income > 1600000)
    {
        tax += 1600000 * 0.21m;
        income -= 1600000;
    }
    else return tax + (income * 0.21m);

    tax += income * 0.24m;

    return tax;
}

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
