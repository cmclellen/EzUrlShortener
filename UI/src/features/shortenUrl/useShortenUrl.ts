import { useMutation, useQueryClient } from "@tanstack/react-query";
import { shortenUrl as shortenUrlApi } from "../../services/apiShortenUrl";
import toast from "react-hot-toast";

export default function useShortenUrl() {
  const queryClient = useQueryClient();
  const { isPending: isShorteningUrl, mutate: shortenUrl } = useMutation({
    mutationFn: shortenUrlApi,
    onSuccess: () => {
      toast.success("Shopping list successfully created");
      queryClient.invalidateQueries({ queryKey: ["url"] });
    },
    onError: (err) => {
      toast.error(err.message);
    },
  });

  return { isShorteningUrl, shortenUrl };
}
