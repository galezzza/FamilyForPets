﻿using FamilyForPets.Core.DTOs;
using FamilyForPets.Files.Shared.DTOs;

namespace FamilyForPets.Files.Contracts.Requests.Upload.Multipart
{
    public record MultipartUploadCompleteRequest(
        FileName FileName,
        string UploadId,
        List<PartETag> ETags);

}
