using CourseService.Application.DTOs;
using CourseService.Application.DTOs.Common;
using CourseService.Application.DTOs.CourseDtos;
using CourseService.Application.DTOs.ModuleDtos;
using CourseService.Application.Interfaces.Repositories;
using CourseService.Application.Interfaces.Services;
using CourseService.Domain.Entities;
using CourseService.Domain.Enums;

namespace CourseService.Application.Services;

public class CourseService(
    ICourseRepository courseRepository,
    ISubscriptionRepository subscriptionRepository,
    TimeProvider timeProvider) : ICourseService
{
    private readonly TimeProvider _timeProvider = timeProvider;
    private readonly ICourseRepository _courseRepository = courseRepository;
    private readonly ISubscriptionRepository _subscriptionRepository = subscriptionRepository;

    public async Task<Guid> CreateCourseAsync(
        Guid authorId, 
        CreateCourseDto createCourseDto, 
        CancellationToken cancellationToken)
    {
        var course = new Course
        {
            Id = Guid.NewGuid(),
            Title = createCourseDto.Title,
            Level = createCourseDto.Level,
            AuthorId = authorId,
            Description = createCourseDto.Description,
            IsPublished = false,
            CreatedAt = _timeProvider.GetUtcNow()
        };
        
        await _courseRepository.AddAsync(course, cancellationToken);
        
        return course.Id;
    }

    public async Task UpdateCourseAsync(
        Guid courseId, 
        Guid userId, 
        UpdateCourseDto updateCourseDto,
        CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(courseId, cancellationToken);

        if (course == null)
            throw new Exception($"Course with ID {courseId} was not found.");
            
        if (course.AuthorId != userId)
            throw new Exception("You are not allowed to edit this course.");
        
        if (updateCourseDto.Title is not null)
            course.Title = updateCourseDto.Title;

        if (updateCourseDto.Description is not null)
            course.Description = updateCourseDto.Description;

        if (updateCourseDto.Level.HasValue)
            course.Level = updateCourseDto.Level.Value;

        if (updateCourseDto.IsPublished.HasValue)
            course.IsPublished = updateCourseDto.IsPublished.Value;

        await _courseRepository.UpdateAsync(course, cancellationToken);
    }

    public async Task DeleteCourseAsync(
        Guid courseId, 
        Guid userId, 
        CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(courseId, cancellationToken);
        
        if (course == null)
            throw new Exception($"Course with ID {courseId} was not found.");
            
        if (course.AuthorId != userId)
            throw new Exception("You are not allowed to delete this course.");
        
        await _courseRepository.DeleteAsync(courseId, cancellationToken);
    }

    public async Task<IEnumerable<CourseSummaryDto>> GetAuthorCoursesAsync(
        Guid authorId, 
        PageRequest request, 
        CancellationToken cancellationToken)
    {
        PagedResult<Course> pagedCourses = await _courseRepository.GetAuthorCoursesAsync(
            authorId,
            request,
            cancellationToken);

        return pagedCourses.Items.Select(course => new CourseSummaryDto(
            course.Id,
            course.Title, 
            course.Level, 
            course.AuthorId, 
            course.CreatedAt
        ));
    }

    public async Task<CourseDetailsDto?> GetCourseByIdAsync(
        Guid courseId, 
        CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetCourseStructureAsync(courseId, cancellationToken);
        
        if (course is null)
        {
            return null;
        }
        
        return new CourseDetailsDto(
            course.Id,
            course.Title,
            course.Description,
            course.Level,
            course.AuthorId,
            course.IsPublished,
            course.CreatedAt,
            course.Modules?.Select(m => new ModuleSummaryDto(
                m.Id,
                m.CourseId,
                m.Title,
                m.Order
            )).ToList() ?? []
        );
    }

    public async Task<IEnumerable<CourseSummaryDto>> GetPublishedCoursesAsync(
        PageRequest request, 
        CancellationToken cancellationToken)
    {
        PagedResult<Course> pagedCourses = await _courseRepository.GetPublishedCoursesAsync(
            request, 
            cancellationToken);

        return pagedCourses.Items.Select(course => new CourseSummaryDto(
            course.Id,
            course.Title,
            course.Level,
            course.AuthorId,
            course.CreatedAt
        ));
    }

    public async Task<IEnumerable<StudentCourseDto>> GetStudentCoursesAsync(
        Guid studentId, 
        PageRequest request, 
        CancellationToken cancellationToken)
    {
        PagedResult<Course> pagedCourses = await _courseRepository.GetStudentCoursesAsync(
            studentId, 
            request, 
            cancellationToken);

        var subscriptions = await _subscriptionRepository.GetUserSubscriptionsAsync(studentId, cancellationToken);
        var subscriptionsDict = subscriptions.ToDictionary(s => s.CourseId);

        var result = new List<StudentCourseDto>();

        foreach (var course in pagedCourses.Items)
        {
            subscriptionsDict.TryGetValue(course.Id, out var subscription);

            var status = subscription?.Status ?? CourseStatus.None;
            var progress = CalculateProgress(subscription);

            result.Add(new StudentCourseDto(
                course.Id,
                course.Title,
                course.Description,
                course.Level,
                status,
                progress
            ));
        }

        return result;
    }

    public async Task<StudentCourseDto?> GetStudentCourseContentAsync(
        Guid studentId, 
        Guid courseId, 
        CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetCourseStructureAsync(courseId, cancellationToken);

        if (course is null)
        {
            throw new Exception($"Course with ID {courseId} was not found.");
        }

        var subscriptions = await _subscriptionRepository.GetUserSubscriptionsAsync(studentId, cancellationToken);
        var subscription = subscriptions.FirstOrDefault(s => s.CourseId == courseId);

        var status = subscription?.Status ?? CourseStatus.None;
        var progress = CalculateProgress(subscription);

        return new StudentCourseDto(
            course.Id,
            course.Title,
            course.Description,
            course.Level,
            status,
            progress
        );
    }
    private static double CalculateProgress(Subscription? subscription)
    {
        if (subscription is null)
        {
            return 0.0;
        }

        return subscription.Status switch
        {
            CourseStatus.Active => 50.0, 
            CourseStatus.Completed  => 100.0,
            _                       => 0.0
        };
    }
}
