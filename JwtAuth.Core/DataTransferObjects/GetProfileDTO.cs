﻿namespace JwtAuth.Core.DataTransferObjects;

public record GetProfileDTO(int ProfileId, string FirstName, string LastName,
    string Email, string Phone, DateTime CreateDate, DateTime? UpdateDate);
