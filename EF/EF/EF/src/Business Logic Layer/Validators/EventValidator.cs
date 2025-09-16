using BusinessLogicLayer.DTOs;
using System;

namespace BusinessLogicLayer.Validators
{
    public static class EventValidator
    {
        public static void ValidateCreate(EventCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new ArgumentException("Title cannot be empty.");

            if (dto.EndTime <= dto.StartTime)
                throw new ArgumentException("EndTime must be greater than StartTime.");
        }

        public static void ValidateUpdate(EventUpdateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new ArgumentException("Title cannot be empty.");

            if (dto.EndTime <= dto.StartTime)
                throw new ArgumentException("EndTime must be greater than StartTime.");
        }
    }
}