import { useMutation, useQueryClient } from "@tanstack/react-query";
import { shortenUrl as shortenUrlApi } from "../../services/apiShortenUrl";
import toast from "react-hot-toast";

export default function useShortenUrl() {
  const queryClient = useQueryClient();
  const { isPending: isShorteningUrl, mutate: shortenUrl } = useMutation({
    mutationFn: shortenUrlApi,
    onSuccess: () => {
      toast.success("URL shortened successfully created");
      queryClient.invalidateQueries({ queryKey: ["urls"] });
    },
    onError: (err) => {
      toast.error(err.message);
    },
  });

  return { isShorteningUrl, shortenUrl };
}
