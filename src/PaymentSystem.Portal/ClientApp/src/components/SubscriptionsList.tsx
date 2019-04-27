import React from "react";
import useFetch from "fetch-suspense";
import Contants from "../Constants";
import { ISubscription } from "../types";
export const SubscriptionList: React.FC = () => {
  const result = useFetch(Contants.subscriptions) as ISubscription[];
  return (
    <ol>
      {result.map(x => (
        <li key={x.id}>{`${x.name} - ${x.fee}`}</li>
      ))}
    </ol>
  );
};
