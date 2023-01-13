using SocialWork.Domain.Entity;
using SocialWork.Domain.Response;
using SocialWork.Domain.ViewModels.Training;

namespace SocialWork.Service.Interfaces;

public interface ITrainingService
{
    Task<BaseResponse<IEnumerable<Training>>> GetTrainings();
    Task<BaseResponse<Training>> GetTraining(int id);
    Task<BaseResponse<Training>> CreateTraining(TrainingViewModel trainingViewModel);
    Task<BaseResponse<bool>> DeleteTraining(int id);
    Task<BaseResponse<Training>> UpdateTraining(int id, TrainingViewModel trainingViewModel);
}