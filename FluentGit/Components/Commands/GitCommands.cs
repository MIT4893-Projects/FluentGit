using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FluentGit.Components.Commands
{
    public static class GitCommands
    {
        /// <summary>
        /// Check if there is a repository at a directory.
        /// </summary>
        /// <param name="directory">A valid directory's path to directory need to check</param>
        /// <returns></returns>
        public static bool ValidRepoAt(string directory)
        {
            try
            {
                _ = new Repository(directory);
            }
            catch (RepositoryNotFoundException)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Initialize a new repository at a directory.
        /// </summary>
        /// <param name="directory">A valid directory's path to directory need to initialize</param>
        public static void Init(string directory)
        {
            Repository.Init(directory);
        }

        public static void Status(string directory)
        {
            Repository repo = new(directory);
            repo.RetrieveStatus(new StatusOptions());
        }

        public static int Clone(string sourceLocation, string targetDirectory)
        {
            try
            {
                Repository.Clone(sourceLocation, targetDirectory);
            }
            catch (LibGit2SharpException exception)
            {
                DialogDisplayer.ShowMessage(exception.Message, "Error");
                return 1;
            }
            return 0;
        }
    }
}
