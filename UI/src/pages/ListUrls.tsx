import { Link } from "react-router-dom";
import useGetUrls from "../features/shortenUrl/useGetUrls";
import PageLayout from "../ui/PageLayout";
import { TiTrash } from "react-icons/ti";
import useDeleteShortenedUrl from "../features/shortenUrl/useDeleteShortenedUrl";
import Spinner from "../ui/Spinner";
import Modal, { useModal } from "../ui/Modal";
import ConfirmDelete from "../ui/ConfirmDelete";

function ListUrls() {
  const { closeModal } = useModal();
  const { isLoadingUrls, urls } = useGetUrls();
  const { deleteShortenedUrl, isDeletingShortenedUrl } =
    useDeleteShortenedUrl();

  function handleDeleteShortCode(shortCode: string) {
    deleteShortenedUrl(shortCode, {
      onSuccess: () => {
        closeModal();
      },
    });
  }

  if (isLoadingUrls || isDeletingShortenedUrl) return <Spinner />;

  return (
    <>
      <PageLayout title="List URLs">
        {urls && !urls.length && !isLoadingUrls && (
          <p className="text-center font-semibold text-stone-600">
            No URLs found
          </p>
        )}
        {urls && (
          <ul>
            {urls &&
              urls.map((item) => (
                <li
                  key={item.shortCode}
                  className="grid grid-cols-4 gap-2 rounded-lg p-2 text-sm even:bg-gray-200"
                >
                  <div className="col-span-3 inline-block overflow-hidden text-ellipsis text-nowrap">
                    {item.originalUrl}
                  </div>
                  <div className="flex items-center justify-between">
                    <Link
                      className="text-blue-600 hover:underline"
                      to={`/api/v1/${item.shortCode}`}
                      target="_blank"
                    >
                      {item.shortCode}
                    </Link>
                    <Modal.Open opensWindowName="delete-url">
                      <TiTrash
                        className="text-xl text-red-800"
                        // onClick={() => handleDeleteShortCode(item.shortCode)}
                      />
                    </Modal.Open>
                    <Modal.Window name="delete-url">
                      <ConfirmDelete
                        resourceName="URL"
                        onDelete={() => handleDeleteShortCode(item.shortCode)}
                      />
                    </Modal.Window>
                  </div>
                </li>
              ))}
          </ul>
        )}
      </PageLayout>
    </>
  );
}

export default ListUrls;
