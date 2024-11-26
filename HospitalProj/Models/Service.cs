using System;

namespace HospitalProj.Models
{
    /// <summary>
    /// Услуги
    /// </summary>
    public class Service
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public int Price { get; set; }
    }

    public class Specialist
    {
        public int Id { get; set; }
        public string Fio { get; set; }
        public string Mobile { get; set; }
        public string JobTitle { get; set; }
    }
    public class Patient
    {
        public int Id { get; set; }
        public string Fio { get; set; }
        public string Sex { get; set; }
        public DateTime BirthDay { get; set; }
        public string Mobile { get; set; }
        public string Gmail { get; set; }
        public string Status { get; set; }
        public string From { get; set; }
    }

    public class Questionnaire
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public string Jaloby { get; set; }
        public string Problems { get; set; }
        public string TherapyTarget { get; set; }
        public string Request { get; set; }
        public string Obstacles { get; set; }
        public string Values { get; set; }
        public string Pursuit { get; set; }
        public string LifeTargets { get; set; }
    }

    public class Recording
    {
        public int Id { get; set; }
        public DateTime PlanDate { get; set; }
        public Patient Patient { get; set; }
        public Specialist Specialist { get; set; }
        public bool Format { get; set; }
        public Service Service { get; set; }
    }
    
    public class MeetingInfo
    {
        public int Id { get; set; }
        public Recording Recording { get; set; }
        public string Feelings { get; set; }
        public string Symptoms { get; set; }
        public string Intervents { get; set; }
        public string Quotes { get; set; }
        public string HomeWork { get; set; }
        public string FeedBack { get; set; }
        public string NextTime { get; set; }
        public string Impression { get; set; }
    }
}