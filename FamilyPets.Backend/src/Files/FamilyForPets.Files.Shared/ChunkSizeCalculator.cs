namespace FamilyForPets.Files.UseCases.Upload.Multipart.Start
{

    public static class ChunkSizeCalculator
    {
        private const long TARGET_CHUNK_SIZE = 100 * 1024 * 1024;
        private const int MAX_CHUNKS = 10_000;

        public static (long ChunkSize, int TotalChunks) Calculate(long fileSize)
        {
            if (fileSize <= TARGET_CHUNK_SIZE)
            {
                return (fileSize, 1);
            }

            long chunkSize = CalculateChunkSize(fileSize);

            int totalChunks = (int)Math.Ceiling((double)fileSize / chunkSize);

            return (chunkSize, totalChunks);
        }

        private static long CalculateChunkSize(long fileSize)
        {
            long chunkSize = (fileSize + MAX_CHUNKS - 1) / MAX_CHUNKS;

            chunkSize = Math.Max(TARGET_CHUNK_SIZE, chunkSize);

            return chunkSize;
        }
    }
}
