import { useMutation, useQueryClient } from "@tanstack/react-query";
import { deleteShortenedUrl as deleteShortenedUrlApi } from "../../services/apiShortenUrl";
import toast from "react-hot-toast";

export default function useDeleteShortenedUrl() {
  const queryClient = useQueryClient();
  const { mutate: deleteShortenedUrl, isPending: isDeletingShortenedUrl } =
    useMutation({
      mutationFn: deleteShortenedUrlApi,
      onSuccess: () => {
        toast.success("URL shortened successfully created");
        queryClient.invalidateQueries({ queryKey: ["url"] });
      },
      onError: (err) => {
        toast.error(err.message);
      },
    });

  return { deleteShortenedUrl, isDeletingShortenedUrl };
}
