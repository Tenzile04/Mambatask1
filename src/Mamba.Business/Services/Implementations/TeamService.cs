﻿using Mamba.Business.Exceptions;
using Mamba.Business.Extencions;
using Mamba.Business.Services.Interfaces;
using Mamba.Core.Models;
using Mamba.Core.Repository.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Business.Services.Implementations
{
    public class TeamService:ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IProfessionService _professionRepository;
        private readonly IWebHostEnvironment _env;
        public TeamService(ITeamRepository teamRepository, IWebHostEnvironment env, IProfessionService professionService)
        {
            _teamRepository = teamRepository;
            _professionRepository = professionService;
            _env = env;
        }
        public async Task CreateAsync(Team team)
        {


            if (team.ImageFile != null)
            {

                if (team.ImageFile.ContentType != "image/png" && team.ImageFile.ContentType != "image/jpeg")
                {
                    throw new InvalidImageFileException("ImageFile", "please select correct file type");
                }

                if (team.ImageFile.Length > 1048576)
                {
                    throw new InvalidImageFileException("ImageFile", "file size should be more lower than 1mb");
                }
            }
            else
            {
                throw new InvalidImageFileException("ImageFile", "image file is must be choosed!! ");
            }

            string folder = "uploads/team";

            string newFileName = Helper.GetFileName(_env.WebRootPath, folder, team.ImageFile);

            team.ImageUrl = newFileName;

            await _teamRepository.CreateAsync(team);
            await _teamRepository.CommitAsync();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Team>> GetAllAsync()
        {
            return await _teamRepository.GetAllAsync(t => t.IsDeleted == false, "Profession");
        }

        public async Task<Team> GetByIdAsync(int id)
        {
            return await _teamRepository.GetByIdAsync(t => t.IsDeleted == false && t.Id == id);
        }

        public async Task SoftDelete(int id)
        {
            Team existTeam = await _teamRepository.GetByIdAsync(t => t.Id == id && t.IsDeleted == false);

            if (existTeam is null) throw new NullReferenceException();

            existTeam.IsDeleted = true;

            await _teamRepository.CommitAsync();
        }

        public async Task UpdateAsync(Team team)
        {
            Team wantedTeam = await _teamRepository.GetByIdAsync(t => t.Id == team.Id && t.IsDeleted == false);

            if (wantedTeam == null) throw new NullReferenceException();

            if (team.ImageFile != null)
            {

                if (team.ImageFile.ContentType != "image/png" && team.ImageFile.ContentType != "image/jpeg")
                {

                    throw new InvalidImageFileException("ImageFile", "please select correct file type");
                }

                if (team.ImageFile.Length > 1048576)
                {
                    throw new InvalidImageFileException("ImageFile", "file size should be more lower than 1mb");
                }

                string folderPath = "uploads/team";

                string newFileName = Helper.GetFileName(_env.WebRootPath, folderPath, team.ImageFile);

                string wantedPath = Path.Combine(_env.WebRootPath, folderPath, wantedTeam.ImageUrl);

                if (File.Exists(wantedPath))
                {
                    File.Delete(wantedPath);
                }

                wantedTeam.ImageUrl = newFileName;
            }


            wantedTeam.FullName = team.FullName;
            wantedTeam.LinkUrl = team.LinkUrl;
            wantedTeam.ProfessionId = team.ProfessionId;

            await _teamRepository.CommitAsync();

        }
    }
}
