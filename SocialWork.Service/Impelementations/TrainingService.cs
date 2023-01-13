using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SocialWork.DAL.Interfaces;
using SocialWork.Domain.Entity;
using SocialWork.Domain.Enum;
using SocialWork.Domain.Response;
using SocialWork.Domain.ViewModels.Training;
using SocialWork.Service.Interfaces;


namespace SocialWork.Service.Impelementations;
//Burda Data training yox view data tipine cevirmeliyem 
public class TrainingService:ITrainingService
{
    private readonly IBaseRepository<Training> _baseRepository;

    private readonly IHostingEnvironment _webHostEnvironment;

    public TrainingService(IBaseRepository<Training> baseRepository,IHostingEnvironment hostingEnvironment)
    {
        _baseRepository = baseRepository;
        _webHostEnvironment = hostingEnvironment;
    }

    
    public async Task<BaseResponse<IEnumerable<Training>>> GetTrainings()
    {
        var baseResponse = new BaseResponse<IEnumerable<Training>>();
        try
        {
            var trainings = await _baseRepository.GetAll().ToListAsync();
            if (trainings.Count == 0)
            {
                baseResponse.Description = "Sorğunuza uyğun məlumat tapılmadı";
                baseResponse.StatusCode = StatusCode.TrainingNotFound;
                return baseResponse;
            }
            else
            {
                baseResponse.StatusCode = StatusCode.Ok;
                baseResponse.Data = trainings;
                return baseResponse;
            }
        }
        catch (Exception e)
        {
            return new BaseResponse<IEnumerable<Training>>()
            {
                Description = $"[GetTrainings] :{e.Message}"
            };
        }
    }

    public async Task<BaseResponse<Training>> GetTraining(int id)
    {
        var baseResponse = new BaseResponse<Training>();
        try
        {
            var training = await _baseRepository.GetAll().FirstOrDefaultAsync(x => x.id == id);
            if (training == null)
            {
                baseResponse.Description = "Sorğunuza uyğun məlumat tapılmadı";
                baseResponse.StatusCode = StatusCode.TrainingNotFound;
                return baseResponse;
            }
            else
            {
                baseResponse.StatusCode = StatusCode.Ok;
                baseResponse.Data = training;
                return baseResponse;
            }
        }
        catch (Exception e)
        {
            return new BaseResponse<Training>()
            {
                Description = $"[GetTraining] :{e.Message}"
            };
        }
    }

    public async Task<BaseResponse<Training>> CreateTraining(TrainingViewModel trainingViewModel)
    {
        var baseResponse = new BaseResponse<Training>();
        try
        {
            string? uniqueFileName = UploadedFile(trainingViewModel);
            var training = new Training()
            {
                name = trainingViewModel.name,
                description = trainingViewModel.description,
                price = trainingViewModel.price,
                startDate = trainingViewModel.startDate,
                imageUrl = uniqueFileName
            };
            await _baseRepository.Create(training);
            return baseResponse;
        }
        catch (Exception e)
        {
            baseResponse.Description = $"[CreateTraining] :{e.Message}";
            baseResponse.StatusCode = StatusCode.InternalServerError;
            return baseResponse;
        }
    }

    public async Task<BaseResponse<bool>> DeleteTraining(int id)
    {
        var baseResponse = new BaseResponse<bool>(){Data = true};
        try
        {
            var training = await _baseRepository.GetAll().FirstOrDefaultAsync(x => x.id == id);
            if (training == null)
            {
                
                baseResponse.Description = "Sorğunuza uyğun məlumat tapılmadı";
                baseResponse.StatusCode = StatusCode.TrainingNotFound;
                baseResponse.Data = false;
                return baseResponse;
            }
            else
            {
                await _baseRepository.Delete(training);
                baseResponse.Description =  "Sorğunuza uyğun məlumat silindi";
                baseResponse.StatusCode = StatusCode.Ok;
                return baseResponse;
            }

        }
        catch (Exception e)
        {
            baseResponse.Data = false;
            baseResponse.Description = $"[DeleteTraining] :{e.Message}";
            baseResponse.StatusCode = StatusCode.InternalServerError;
            return baseResponse;
        }
    }

    public async Task<BaseResponse<Training>> UpdateTraining(int id, TrainingViewModel trainingViewModel)
    {
        var baseResponse = new BaseResponse<Training>();
          
        try
        {
            var training = await _baseRepository.GetAll().FirstOrDefaultAsync(x => x.id == id);
            if (training == null)
            {
                baseResponse.StatusCode = StatusCode.TrainingNotFound;
                baseResponse.Description = "Sorğunuza uyğun məlumat tapılmadı";
                return baseResponse;
            }

            else
            { 
                string? uniqueFileName = UploadedFile(trainingViewModel);
                training.name = trainingViewModel.name;
                training.description = trainingViewModel.description;
                training.price = trainingViewModel.price;
                training.startDate = trainingViewModel.startDate; 
                training.imageUrl = uniqueFileName;

                await _baseRepository.Update(training);
                
                return baseResponse;
            }
        }
        catch (Exception e)
        {
            baseResponse.Description = $"[UpdateTraining] :{e.Message}";
            baseResponse.StatusCode = StatusCode.InternalServerError;
            return baseResponse;
        }
    }

    
    
    private string? UploadedFile(TrainingViewModel trainingViewModel )
    {
        string? uniqueFileName = trainingViewModel.imageUrl;

        if (trainingViewModel.image != null)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            if (trainingViewModel.imageUrl != null)
            {
                uniqueFileName = trainingViewModel.imageUrl;
                string filePath = Path.Combine(uploadsFolder, trainingViewModel.imageUrl);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                  
                    trainingViewModel.image.CopyTo(fileStream);
                }
            }
            else
            {  
                uniqueFileName = Guid.NewGuid().ToString() + "_" + trainingViewModel.image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    trainingViewModel.image.CopyTo(fileStream);
                }
            }    
          
        }

        return uniqueFileName;
    }

    // private bool DeleteOldImagePath(TrainingViewModel trainingViewModel)
    // {
    //     if (trainingViewModel.imageUrl != null)
    //     {
    //         string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
    //         string? oldImagePath = Path.Combine(uploadsFolder, trainingViewModel.imageUrl);
    //         if (System.IO.File.Exists(oldImagePath))
    //         {
    //             System.IO.File.Delete(oldImagePath);
    //             return true;
    //         }
    //         
    //     }
    //     return false;
    // }
}