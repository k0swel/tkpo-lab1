// See https://aka.ms/new-console-template for more information

using Teacher.Services;
using Teacher.Repositories;
using Teacher.Entities;
using Group.Repositories;
using Group.Services;
using Student.Entities;
using Student.Services;
using Course.Entities;

class Program {
    static void Main(string[] args) {
        lab1.Application app = lab1.Application.Initialize(lab1.Application.RepositoryType.IN_MEMORY);
        app.EventLoop();
    }
}