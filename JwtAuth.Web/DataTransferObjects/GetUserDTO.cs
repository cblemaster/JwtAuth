﻿namespace JwtAuth.Web.DataTransferObjects;

public record GetUserDTO(int UserId, int ProfileId, string Username, 
    DateTime CreateDate, DateTime? UpdateDate, GetProfileDTO Profile, 
    IEnumerable<GetRoleDTO> Roles);
