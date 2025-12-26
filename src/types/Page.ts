export type Page = {
  id: number;
  slug: string;
  title: string;
  content: string;
  description: string;
  publishedOn: string | Date;
  updatedOn: string | Date;
  expiresOn: string | Date;
};
