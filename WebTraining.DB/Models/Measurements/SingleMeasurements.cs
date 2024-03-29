﻿namespace WebTraining.DB.Models.Measurements
{
    public class SingleMeasurements
    {
        public int ID { get; set; }

        public DateTime Date { get; set; }

        public int MuscleId { get; set; }

        public MusclesMeasurements TypeOfMuscle { get; set; }

        public float Value { get; set; }

        public float Change { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
