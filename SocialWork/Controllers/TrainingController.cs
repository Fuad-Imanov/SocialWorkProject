using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialWork.DAL.Interfaces;
using SocialWork.DAL.Repository;
using SocialWork.Domain.Entity;
using SocialWork.Domain.ViewModels.Training;
using SocialWork.Service.Interfaces;
using System.Security.Cryptography.X509Certificates;

namespace SocialWork.Controllers;

public class TrainingController : Controller
{
    private readonly ITrainingService _trainingService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public TrainingController(ITrainingService trainingService,IWebHostEnvironment hostingEnvironment)
    {
        _trainingService = trainingService;
        _webHostEnvironment = hostingEnvironment;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _trainingService.GetTrainings();
        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            return View(response?.Data?.ToList());   
        }
        else
        {
            return RedirectToAction("Error");
        }
        
    }

    [HttpGet]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _trainingService.GetTraining(id);
        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            return View(response.Data);
        }
        else
        {
            return RedirectToAction("Error");
        }
    }
    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _trainingService.DeleteTraining(id);
        if (response.StatusCode==Domain.Enum.StatusCode.Ok)
        {
            return RedirectToAction("GetAll");
        }
        else
        {
            return RedirectToAction("Error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Save(int id)
    {
        var trainingViewModel = new TrainingViewModel();
        if (id == 0)
        {

            return View(trainingViewModel);
        }
        else
        {

            var response = await _trainingService.GetTraining(id);
            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            {
              
                trainingViewModel.id = response.Data.id;
                trainingViewModel.name = response.Data.name;
                trainingViewModel.startDate = response.Data.startDate;
                trainingViewModel.description = response.Data.description;
                trainingViewModel.price = response.Data.price;
                trainingViewModel.imageUrl = response.Data.imageUrl;
                return View(trainingViewModel);
            }

            return RedirectToAction("Error");
        }
        
    }

    [HttpPost]
    public async Task<IActionResult> Save(TrainingViewModel trainingViewModel)
    {
        if (ModelState.IsValid)
        {
            if (trainingViewModel.id == 0)
            {
                await _trainingService.CreateTraining(trainingViewModel);
            }
            else
            {
                await _trainingService.UpdateTraining(trainingViewModel.id, trainingViewModel);
            }
        }
        return RedirectToAction("GetAll");
    }

    public IActionResult Error()
    {
        return View();
    }

    
}