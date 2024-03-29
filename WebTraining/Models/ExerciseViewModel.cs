﻿using WebTraining.Core.DTO;

namespace WebTraining.Models
{
    public class ExerciseViewModel
    {
        public IEnumerable<ExerciseDTO> Exercises { get; set; }
        public IEnumerable<ImageExerciseDTO> Image { get; set; }
    }
}
