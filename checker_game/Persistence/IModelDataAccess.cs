using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace checker_game.Persistence
{
    /// <summary>
    /// File manager.
    /// </summary>
    public interface IModelDataAccess
    {
        /// <summary>
        /// File loading
        /// </summary>
        /// <param name="path">Reaching path</param>
        /// <returns>Scanned table</returns>
        Task<ModelTable> LoadAsync(String path);

        /// <summary>
        /// File saving
        /// </summary>
        /// <param name="path">Reaching path.</param>
        /// <param name="table">Table to be saved</param>
        Task SaveAsync(String path, ModelTable table);
    }
}
