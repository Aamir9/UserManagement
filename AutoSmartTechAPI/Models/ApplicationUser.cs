using AutoSmartTechAPI.UserComm;
using DataAccessLayer.DataEntities;
using Microsoft.AspNet.Identity;
using System;
namespace AutoSmartTechAPI.Models
{
    public class ApplicationUser : User,IUser<Guid>
    {
        public string UserName { get; set; }
        public  bool EmailConfirmed { get; set; } = false;
        public ApplicationUser()
        {
            UserName = EmailAddress;

        }


        //public static ApplicationUser MapUserToApplicationUser(User user)
        //{
        //    try
        //    {

        //        ApplicationUser appUser = new ApplicationUser();
        //        appUser.Id = user.Id;
        //        appUser.EmailAddress = user.EmailAddress;
        //        appUser.Password = user.Password;
        //        appUser.UserTypeId = user.UserTypeId;
        //        appUser.FirstName = user.FirstName;
        //        appUser.CreatedOn = user.CreatedOn;
        //        appUser.AppStatusId = user.AppStatusId;
        //        appUser.Counter = user.Counter;

        //        appUser.LastName = user.LastName;
        //        appUser.DateOfBirth = user.DateOfBirth;
        //        appUser.GenderId = user.GenderId;
        //        appUser.BusinessName = user.BusinessName;
        //        appUser.MobileNumber = user.MobileNumber;
        //        appUser.TelephoneNumber = user.TelephoneNumber;
        //        appUser.CountryId = user.CountryId;

        //        if (user.UpdatedOn != null)
        //            appUser.UpdatedOn = user.UpdatedOn;
        //        appUser.UpdatedBy = user.UpdatedBy;

        //        if (user.ParentUserId != null)
        //            appUser.ParentUserId = user.ParentUserId;
        //        appUser.ResetPasswordToken = user.ResetPasswordToken;
        //        appUser.TimeZoneName = user.TimeZoneName;
        //        appUser.TimeZoneOffset = user.TimeZoneOffset;
        //        if (user.LastStatusUpdatedOn != null)
        //            appUser.LastStatusUpdatedOn = user.LastStatusUpdatedOn;
        //        appUser.LastStatusUpdatedBy = user.LastStatusUpdatedBy;
        //        appUser.TempPassword = user.TempPassword;
        //        appUser.ReferralUserId = user.ReferralUserId;
        //        appUser.IsSamePostalAddress = user.IsSamePostalAddress;
        //        if (user.ReferralUserId != null)
        //            appUser.ReferenceCode = user.ReferenceCode;
        //        if (user.NotShowReferralCodePopUp != null)
        //            appUser.NotShowReferralCodePopUp = user.NotShowReferralCodePopUp;


        //        return appUser;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public static User MapApplicationUserToUser(ApplicationUser appUser)
        //{
        //    try
        //    {
        //        User user = null;
        //        if (appUser != null)
        //        {
        //            // user = DynamicMapping.MapSamePropertiesDynamic<User>(appUser);
        //        }
        //        return user;

        //        //User user = new User();

        //        //user.Id = appUser.Id;
        //        //user.EmailAddress = appUser.UserName;
        //        //user.Password = appUser.Password;
        //        //user.UserTypeId= appUser.UserTypeId;
        //        //user.FirstName = appUser.FirstName;
        //        //user.CreatedOn = appUser.CreatedOn;
        //        //user.AppStatusId = appUser.AppStatusId;
        //        //user.Counter = appUser.Counter;

        //        //user.LastName = appUser.LastName;
        //        //user.DateOfBirth = appUser.DateOfBirth;
        //        //user.GenderId = appUser.GenderId;
        //        //user.BusinessName = appUser.BusinessName;
        //        //user.MobileNumber = appUser.MobileNumber;
        //        //user.TelephoneNumber = appUser.TelephoneNumber;
        //        //user.CountryId = appUser.CountryId;

        //        //if (appUser.UpdatedOn != null)
        //        //    user.UpdatedOn = appUser.UpdatedOn;
        //        //user.UpdatedBy = appUser.UpdatedBy;

        //        //if (appUser.ParentUserId != null)
        //        //    user.ParentUserId = appUser.ParentUserId;
        //        //user.ResetPasswordToken = appUser.ResetPasswordToken;
        //        //user.TimeZoneName = appUser.TimeZoneName;
        //        //user.TimeZoneOffset = appUser.TimeZoneOffset;
        //        //if (appUser.LastStatusUpdatedOn != null)
        //        //    user.LastStatusUpdatedOn = appUser.LastStatusUpdatedOn;
        //        //user.LastStatusUpdatedBy = appUser.LastStatusUpdatedBy;
        //        //user.TempPassword = appUser.TempPassword;
        //        //user.ReferralUserId = appUser.ReferralUserId;
        //        //user.IsSamePostalAddress = appUser.IsSamePostalAddress;
        //        //if (appUser.ReferralUserId != null)
        //        //    user.ReferenceCode = appUser.ReferenceCode;
        //        //if (appUser.NotShowReferralCodePopUp != null)
        //        //    user.NotShowReferralCodePopUp = appUser.NotShowReferralCodePopUp;



        //        //  user.AdditionalCharges = appUser.AdditionalCharges;
        //        //user.Administrator = appUser.Administrator;
        //        //user.AuditTrails = appUser.AuditTrails;
        //        //user.BookerInvites = appUser.BookerInvites;
        //        //user.BookerNotes = appUser.BookerNotes;
        //        //user.BookerNotes1 = appUser.BookerNotes1;
        //        //user.Candidate = appUser.Candidate;
        //        //user.CandidateParent = appUser.CandidateParent;
        //        //user.CertificateLoggings  =appUser.CertificateLoggings;
        //        //user.ConnectivityCallRequests = appUser.ConnectivityCallRequests;
        //        //user.Countries = appUser.Countries;
        //        //user.Countries1 = appUser.Countries1;
        //        //user.Country = appUser.Country;
        //        //user.Exams = appUser.Exams;
        //        //user.Exams1 = appUser.Exams1;
        //        //user.ExamBookings = appUser.ExamBookings;
        //        //user.ExamBookings1 = appUser.ExamBookings1;
        //        //user.ExamBookings1 = appUser.ExamBookings1;
        //        //user.ExamBookings2 = appUser.ExamBookings2;
        //        //user.ExamBookings3 = appUser.ExamBookings3;
        //        //user.ExamBookingAdminInfoes = appUser.ExamBookingAdminInfoes;
        //        //user.ExamBookingNotes = appUser.ExamBookingNotes;
        //        //user.ExamCandidates = appUser.ExamCandidates;
        //        //user.ExamCandidates1 = appUser.ExamCandidates1;
        //        //user.ExamCandidateEvidenceFiles = appUser.ExamCandidateEvidenceFiles;
        //        //user.ExamCandidateEvidenceFiles1 = appUser.ExamCandidateEvidenceFiles1;
        //        //user.ExamCandidateExamFiles = appUser.ExamCandidateExamFiles;
        //        //user.ExamCandidateExamFiles1 = appUser.ExamCandidateExamFiles1;
        //        //user.ExamCandidateInvitationTokens = appUser.ExamCandidateInvitationTokens;
        //        //user.ExamCandidateParentTokens = appUser.ExamCandidateParentTokens;
        //        //user.ExamCandidateParentTokens1 = appUser.ExamCandidateParentTokens1;
        //        //user.ExamCategories = appUser.ExamCategories;
        //        //user.ExamCategories1 = appUser.ExamCategories1;
        //        //user.ExamCentres = appUser.ExamCentres;
        //        //user.ExamCentres1 = appUser.ExamCentres1;
        //        //user.Examiner = appUser.Examiner;
        //        //user.ExaminerNotes = appUser.ExaminerNotes;
        //        //user.ExamPayments = appUser.ExamPayments;
        //        //user.ExamPaymentRefunds = appUser.ExamPaymentRefunds;
        //        //user.ExamRegions = appUser.ExamRegions;
        //        //user.ExamRegions1 = appUser.ExamRegions1;
        //        //user.ExamSessions = appUser.ExamSessions;
        //        //user.ExamSessions1 = appUser.ExamSessions1;
        //        //user.ExamTypes = appUser.ExamTypes;
        //        //user.ExamTypes1 = appUser.ExamTypes1;
        //        //user.Gender = appUser.Gender;
        //        //user.InstrumentGrades = appUser.InstrumentGrades;
        //        //user.InstrumentGrades1 = appUser.InstrumentGrades1;
        //        //user.InstrumentGradeFees = appUser.InstrumentGradeFees;
        //        //user.InstrumentGradeFees1 = appUser.InstrumentGradeFees1;
        //        //user.InstrumentGradeFeeUsers = appUser.InstrumentGradeFeeUsers;
        //        //user.InstrumentGradeFeeUsers1 = appUser.InstrumentGradeFeeUsers1;
        //        //user.IssueCertificates = appUser.IssueCertificates;
        //        //user.MusicInstruments = appUser.MusicInstruments;
        //        //user.MusicInstruments1 = appUser.MusicInstruments1;



        //        //user.ParentUserId = appUser.ParentUserId;
        //        //if (appUser.Organizations != null)
        //        //{
        //        //    foreach (var orgItem in appUser.Organizations)
        //        //    {
        //        //        var org = new Organization();
        //        //        org.Id = orgItem.Id;
        //        //        org.Name = orgItem.Name;
        //        //        user.Organizations.Add(org);
        //        //    }
        //        //}

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}
