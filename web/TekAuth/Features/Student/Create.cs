namespace TekAuth.Features.Student
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using DAL;
    using FluentValidation;
    using MediatR;
    using Tekconf.Data.Entities;

    public class Create
    {
        public class Command : IRequest
        {
            public string LastName { get; set; }

            [Display(Name = "First Name")]
            public string FirstMidName { get; set; }

            public DateTime? EnrollmentDate { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(m => m.LastName).NotNull().Length(1, 50);
                RuleFor(m => m.FirstMidName).NotNull().Length(1, 50);
                RuleFor(m => m.EnrollmentDate).NotNull();
            }
        }

        public class Handler : RequestHandler<Command>
        {
            private readonly ConferenceContext _db;
            private readonly IMapper _mapper;

            public Handler(ConferenceContext db, IMapper mapper)
            {
                _db = db;
                _mapper = mapper;
            }

            protected override void HandleCore(Command message)
            {
                var student = _mapper.Map<Command, Student>(message);

                _db.Students.Add(student);
            }
        }
    }
}