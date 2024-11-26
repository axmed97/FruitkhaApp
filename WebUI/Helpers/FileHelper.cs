namespace WebUI.Helpers
{
    public static class FileHelper
    {
        public static async Task<string> SaveFileAsync(this IFormFile file, string WebRootPath, string folder) 
        {
            var filePath = Path.Combine(WebRootPath, folder).ToLower();
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            var path = $"/{folder}/" + Guid.NewGuid() + file.FileName;
            using FileStream fileStream = new(WebRootPath + path, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return path;
        }
    }
}
