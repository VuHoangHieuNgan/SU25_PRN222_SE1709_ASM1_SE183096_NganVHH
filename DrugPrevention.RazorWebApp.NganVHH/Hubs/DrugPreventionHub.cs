using DrugPrevention.Repositories.NganVHH.Models;
using DrugPrevention.Services.NganVHH;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
namespace DrugPrevention.RazorWebApp.NganVHH.Hubs
{
    public class DrugPreventionHub : Hub
    {
        private readonly IAppointmentsNganVHHService _appointmentsNganVHHService;
        private readonly IConsultantsTrongLHService _consultantsTrongLHService;
        private readonly IUsersTuyenTMService _usersTuyenTMService;

        public DrugPreventionHub(IAppointmentsNganVHHService appointmentsNganVHHService, IConsultantsTrongLHService consultantsTrongLHService, IUsersTuyenTMService usersTuyenTMService)
        {
            _appointmentsNganVHHService = appointmentsNganVHHService;
            _consultantsTrongLHService = consultantsTrongLHService;
            _usersTuyenTMService = usersTuyenTMService;
        }


        public async Task HubDelete_AppointmentsNganVHH(string appointmentNganVHHID) 
        {
            //Gửi ID cho tất cả các client có kết nối
            await Clients.All.SendAsync("Receiver_DeleteAppointments", appointmentNganVHHID);

            await _appointmentsNganVHHService.DeleteAsync(int.Parse(appointmentNganVHHID));
        }

        public async Task HubCreate_AppointmentsNganVHH(string appointmentNganVHHJsonString)
        {
            var item = JsonConvert.DeserializeObject<AppointmentsNganVHH>(appointmentNganVHHJsonString);
            if (item == null)
            {
                // Optionally, handle error or notify client
                return;
            }

            await _appointmentsNganVHHService.CreateAsync(item);

            var nganVHH = await _appointmentsNganVHHService.GetByIdAsync(item.AppointmentNganVHHID);
            if (nganVHH != null)
            {
                // Prevent object cycle by sending a DTO or anonymous object
                var result = new
                {
                    nganVHH.AppointmentNganVHHID,
                    UserEmail = nganVHH.User.Email,
                    ConsultantEmail = nganVHH.Consultant.User.Email,
                    nganVHH.AppointmentDateTime,
                    nganVHH.Duration,
                    nganVHH.ConsultationType,
                    nganVHH.AssessmentType,
                    nganVHH.IsInterpreterNeeded,
                    nganVHH.PrimaryIssue,
                    nganVHH.RiskLevel,
                    nganVHH.Status,
                    nganVHH.FeedbackRating,
                    nganVHH.Notes,
                    nganVHH.RecordingLink
                };

                await Clients.All.SendAsync("Receiver_CreateAppointments", result);
            }
        }

        public async Task HubUpdate_AppointmentsNganVHH(string appointmentNganVHHJsonString)
        {
            var item = JsonConvert.DeserializeObject<AppointmentsNganVHH>(appointmentNganVHHJsonString);

            await _appointmentsNganVHHService.UpdateAsync(item);

            var nganVHH = await _appointmentsNganVHHService.GetByIdAsync(item.AppointmentNganVHHID);
            if (nganVHH != null)
            {
                // Prevent object cycle by sending a DTO or anonymous object
                var result = new
                {
                    nganVHH.AppointmentNganVHHID,
                    UserEmail = nganVHH.User.Email,
                    ConsultantEmail = nganVHH.Consultant.User.Email,
                    nganVHH.Consultant.Specialization,
                    nganVHH.AppointmentDateTime,
                    nganVHH.Duration,
                    nganVHH.ConsultationType,
                    nganVHH.AssessmentType,
                    nganVHH.IsInterpreterNeeded,
                    nganVHH.PrimaryIssue,
                    nganVHH.RiskLevel,
                    nganVHH.Status,
                    nganVHH.FeedbackRating,
                    nganVHH.Notes,
                    nganVHH.RecordingLink
                };
                await Clients.All.SendAsync("Receiver_UpdateAppointments", result);
            }
        }

    }
}
