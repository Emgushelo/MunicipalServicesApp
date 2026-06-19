using MunicipalServicesApp.DataStructures;
using MunicipalServicesApp.Models;

namespace MunicipalServicesApp
{
    internal static class ServiceRequestStatusFormHelpers
    {
        private static MinHeap<ServiceRequest> _priorityQueue = new MinHeap<ServiceRequest>();
    }
}