type LocalStorageKey = "coffeeshop-access-token-metadata";

export const setLocalStorageItem = ({ key, value }: { key: LocalStorageKey; value: unknown }) =>
  localStorage.setItem(key, JSON.stringify(value));

export const removeLocalStorageItem = ({ key }: { key: LocalStorageKey }) => localStorage.removeItem(key);

export const getLocalStorageItem = <T>({ key }: { key: LocalStorageKey }): T | null => {
  const value = localStorage.getItem(key);
  if (!value) {
    return null;
  }

  return JSON.parse(value);
};
