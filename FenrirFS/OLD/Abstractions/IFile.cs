using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace OLD.FenrirFS
{
    /// <summary>
    /// Represents a file in the file system. Has two modes: a traditional C-style mode (functions
    /// prefixed by a T) and a modern mode (functions prefixed by an M).
    /// </summary>
    public interface IFile : IDisposable
    {
        #region Public Properties

        /// <summary>
        /// Gets the full path of the file.
        /// </summary>
        /// <value>The full path.</value>
        string FullPath { get; }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Gets the path of the file.
        /// </summary>
        /// <value>The path.</value>
        string Path { get; }

        /// <summary>
        /// Gets the stream representing the file, if its open.
        /// </summary>
        /// <value>The stream.</value>
        Stream Stream { get; }

        /// <summary>
        /// Gets a value indicating whether the open file is at the end of the stream or not.
        /// </summary>
        /// <value><c>true</c> if end of file; otherwise, <c>false</c>.</value>
        bool TEOF { get; }

        /// <summary>
        /// Gets the file mode for the open file.
        /// </summary>
        /// <value>The file mode.</value>
        FileMode TFileMode { get; }

        /// <summary>
        /// Gets a value indicating whether the file is open.
        /// </summary>
        /// <value><c>true</c> if the file is open; otherwise, <c>false</c>.</value>
        bool TIsOpen { get; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Modern Mode, Asynchronous: Copies the current file to the new path and with the new name.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="maxTries">The maximum tries to create a new unique name of the form {name (x)} before failing.</param>
        /// <returns>
        /// A boolean task to run to copy the file. The boolean represents whether the file was
        /// copied (true) or not (false).
        /// </returns>
        Task<bool> MCopyAsync(string destination, CollisionOption collisionOption, CancellationToken cancellationToken = default(CancellationToken), int maxTries = 1000);

        /// <summary>
        /// Modern Mode, Synchronous: Copies the current file to the new path and with the new name.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <param name="maxTries">The maximum tries to create a new unique name of the form {name (x)} before failing.</param>
        /// <returns>
        /// A boolean representing whether the file was copied (true) or not (false).
        /// </returns>
        bool MCopySync(string destination, CollisionOption collisionOption, int maxTries = 1000);

        /// <summary>
        /// Modern Mode, Asynchronous: Deletes the current file.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A boolean task to delete the file. The boolean represents whether the file was deleted
        /// (true) or not (false).
        /// </returns>
        Task<bool> MDeleteAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Modern Mode, Synchronous: Deletes the current file.
        /// </summary>
        /// <returns>A boolean representing whether the file was deleted (true) or not (false).</returns>
        bool MDeleteSync();

        /// <summary>
        /// Modern Mode, Asynchronous: Moves the open file to the new path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A boolean task to move the file. The boolean represents whether the file was moved
        /// (true) or not (false).
        /// </returns>
        Task<bool> MMoveAsync(string path, CollisionOption collisionOption, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Modern Mode, Synchronous: Moves the open file to the new path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns>A boolean representing whether the file was moved (true) or not (false).</returns>
        bool MMoveSync(string path, CollisionOption collisionOption);

        /// <summary>
        /// Modern Mode, Asynchronous: Opens the file.
        /// </summary>
        /// <param name="fileAccess">The file access.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A Stream task to open the file. The Stream represents the file, opened in the modes
        /// asked for.
        /// </returns>
        Task<Stream> MOpenAsync(FileAccess fileAccess, FileMode fileMode, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Modern Mode, Synchronous: Opens the file.
        /// </summary>
        /// <param name="fileAccess">The file access.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <returns>A Stream representing the file, opened in the modes asked for.</returns>
        Stream MOpenSync(FileAccess fileAccess, FileMode fileMode);

        /// <summary>
        /// Modern Mode, Asynchronous: Reads the entire file.
        /// </summary>
        /// <returns>
        /// A string task to read the file. The string represents the contents of the file.
        /// </returns>
        Task<string> MReadAllAsync();

        /// <summary>
        /// Modern Mode, Synchronous: Reads the entire file.
        /// </summary>
        /// <returns>A string representing the entire file.</returns>
        string MReadAllSync();

        /// <summary>
        /// Modern Mode, Asynchronous: Renames the current file.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A boolean task to rename the file. The boolean represents whether the file was renamed
        /// (true) or not (false).
        /// </returns>
        Task<bool> MRenameAsync(string name, CollisionOption collisionOption, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Modern Mode, Synchronous: Renames the current file.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns>A boolean representing whether the file was renamed (true) or not (false).</returns>
        bool MRenameSync(string name, CollisionOption collisionOption);

        /// <summary>
        /// Modern Mode, Asynchronous: Writes content to the file.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A boolean task to write text the file. The boolean represents whether the text was
        /// written (true) or not (false).
        /// </returns>
        Task<bool> MWriteAsync(string contents, WriteMode writeMode, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Modern Mode, Asynchronous: Writes content to the file and adds a new line character at
        /// the end.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A boolean task to write text the file. The boolean represents whether the text was
        /// written (true) or not (false).
        /// </returns>
        Task<bool> MWriteLineAsync(string contents, WriteMode writeMode, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Modern Mode, Synchronous: Writes content to the file and adds a new line character at
        /// the end.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <returns>A boolean representing whether the content was written (true) or not (false).</returns>
        bool MWriteLineSync(string contents, WriteMode writeMode);

        /// <summary>
        /// Modern Mode, Synchronous: Writes content to the file.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <returns>A boolean representing whether the content was written (true) or not (false).</returns>
        bool MWriteSync(string contents, WriteMode writeMode);

        /// <summary>
        /// Traditional Mode: Closes the file.
        /// </summary>
        /// <returns>A boolean representing whether the file was closed (true) or not (false).</returns>
        bool TClose();

        /// <summary>
        /// Traditional Mode: Copies the open file to the new path and with the new name.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns>A boolean representing whether the file was copied (true) or not (false).</returns>
        /// <param name="maxTries">The maximum tries to create a new unique name of the form {name (x)} before failing.</param>
        bool TCopy(string destination, CollisionOption collisionOption, int maxTries = 1000);

        /// <summary>
        /// Traditional Mode: Deletes the open file.
        /// </summary>
        /// <returns>A boolean representing whether the file was deleted (true) or not (false).</returns>
        bool TDelete();

        /// <summary>
        /// Traditional Mode: Moves the open file to the new path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns>A boolean representing whether the file was moved (true) or not (false).</returns>
        bool TMove(string path, CollisionOption collisionOption);

        /// <summary>
        /// Traditional Mode: Opens the file.
        /// </summary>
        /// <param name="fileAccess">The file access.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <returns>A boolean representing whether the file was opened (true) or not (false).</returns>
        bool TOpen(FileAccess fileAccess, FileMode fileMode);

        /// <summary>
        /// Traditional Mode: Reads the entire file.
        /// </summary>
        /// <returns>A string representing the entire file.</returns>
        string TReadAll();

        /// <summary>
        /// Traditional Mode: Reads the next line in the file.
        /// </summary>
        /// <returns>A string representing the next line in the file.</returns>
        string TReadLine();

        /// <summary>
        /// Traditional Mode: Renames the opened file.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns>A boolean representing whether the file was renamed (true) or not (false).</returns>
        bool TRename(string name, CollisionOption collisionOption);

        /// <summary>
        /// Traditional Mode: Sets the stream to the beginning of the file.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the stream was reset to the beginning of the stream
        /// (true) or not (false).
        /// </returns>
        bool TSetStreamToFileBeginning();

        /// <summary>
        /// Traditional Mode: Sets the stream to the end of the file.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the stream was set to the end of the stream (true) or not (false).
        /// </returns>
        bool TSetStreamToFileEnd();

        /// <summary>
        /// Traditional Mode: Sets the stream to the position.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns>
        /// A boolean representing whether the stream was set to the desired position of the stream
        /// (true) or not (false).
        /// </returns>
        bool TSetStreamToPosition(long position);

        /// <summary>
        /// Traditional Mode: Writes content to the file.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <returns>A boolean representing whether the content was written (true) or not (false).</returns>
        bool TWrite(string contents);

        /// <summary>
        /// Traditional Mode: Writes content to the file and adds a new line character at the end.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <returns>A boolean representing whether the content was written (true) or not (false).</returns>
        bool TWriteLine(string contents);

        #endregion Public Methods

    }
}