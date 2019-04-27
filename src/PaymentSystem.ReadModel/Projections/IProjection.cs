using System;

namespace PaymentSystem.ReadModel.Projections
{
    public interface IProjection
    {
        Guid ProjectionId { get; set; }
    }
}