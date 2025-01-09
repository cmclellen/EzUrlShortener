import { useQuery } from "@tanstack/react-query";
import { getUrls } from "../../services/apiShortenUrl";

export default function useGetUrls() {
  const { data: urls, isLoading: isLoadingUrls } = useQuery({
    queryKey: ["urls"],
    queryFn: getUrls,
  });

  return { urls, isLoadingUrls };
}
