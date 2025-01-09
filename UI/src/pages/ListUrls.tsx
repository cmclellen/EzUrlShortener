import { Link } from "react-router-dom";
import useGetUrls from "../features/shortenUrl/useGetUrls";
import PageLayout from "../ui/PageLayout";

function ListUrls() {
  const { isLoadingUrls, urls } = useGetUrls();

  return (
    <PageLayout title="List URLs">
      {isLoadingUrls && <p>Loading...</p>}

      <div className="grid grid-cols-3 gap-4 text-stone-700">
        {urls &&
          urls.map((item) => (
            <>
              <div className="col-span-2">{item.originalUrl}</div>
              <div className="text-end">
                <Link
                  className="text-blue-600 hover:underline"
                  to={`/api/v1/${item.shortCode}`}
                  target="_blank"
                >
                  {item.shortCode}
                </Link>
              </div>
            </>
          ))}
      </div>
    </PageLayout>
  );
}

export default ListUrls;
