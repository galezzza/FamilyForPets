﻿using FamilyForPets.Core.Validation;
using FamilyForPets.SharedKernel;
using FamilyForPets.SharedKernel.ValueObjects;
using FluentValidation;

namespace FamilyForPets.Files.UseCases.Download
{
    public class GetPresignedUrlToDownloadFullFileFromFileServiceCommandValidator
        : AbstractValidator<GetPresignedUrlToDownloadFullFileFromFileServiceCommand>
    {
        public GetPresignedUrlToDownloadFullFileFromFileServiceCommandValidator()
        {
            RuleFor(c => c.FileName.Key)
                .NotEmpty()
                .WithError(Errors.General.CannotBeEmpty(nameof(FileName.Key)));

            RuleFor(c => c.FileName.BucketName)
                .NotEmpty()
                .WithError(Errors.General.CannotBeEmpty(nameof(FileName.BucketName)));
        }
    }
}
