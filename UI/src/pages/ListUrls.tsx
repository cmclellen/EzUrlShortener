import { Link } from "react-router-dom";
import useGetUrls from "../features/shortenUrl/useGetUrls";
import PageLayout from "../ui/PageLayout";
import { TiTrash } from "react-icons/ti";
import useDeleteShortenedUrl from "../features/shortenUrl/useDeleteShortenedUrl";

function ListUrls() {
  const { isLoadingUrls, urls } = useGetUrls();
  const { deleteShortenedUrl } = useDeleteShortenedUrl();

  function handleDeleteShortCode(shortCode: string) {
    deleteShortenedUrl(shortCode);
  }

  return (
    <PageLayout title="List URLs">
      {isLoadingUrls && <p>Loading...</p>}

      {urls && !urls.length && !isLoadingUrls && (
        <p className="text-center font-semibold text-stone-600">
          No URLs found
        </p>
      )}
      {urls && (
        <div className="grid grid-cols-5 gap-4 text-stone-700">
          {urls &&
            urls.map((item) => (
              <>
                <div className="col-span-3">{item.originalUrl}</div>
                <div className="flex justify-end">
                  <Link
                    className="text-blue-600 hover:underline"
                    to={`/api/v1/${item.shortCode}`}
                    target="_blank"
                  >
                    {item.shortCode}
                  </Link>
                </div>
                <div className="flex justify-center text-red-800">
                  <TiTrash
                    className="text-xl"
                    onClick={() => handleDeleteShortCode(item.shortCode)}
                  />
                </div>
              </>
            ))}
        </div>
      )}
    </PageLayout>
  );
}

export default ListUrls;
