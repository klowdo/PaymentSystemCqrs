import React, { Suspense } from "react";
import { SubscriptionList } from "./components/SubscriptionsList";
import { CardWithTransactions } from "./components/CardWithTransactions";
import { CardList } from "./components/CardList";
import { BrowserRouter as Router, Route, Link } from "react-router-dom";
const App: React.FC = () => {
  return (
    <Router>
      <div>
        <nav>
          <ul>
            <li>
              <Link to="/">Home</Link>
            </li>
            <li>
              <Link to="/subscriptions/">Subscriptions</Link>
            </li>
            <li>
              <Link to="/cards/">Cards</Link>
            </li>
          </ul>
        </nav>
        <Suspense fallback={"Loading..."}>
          <Route path="/" exact component={Index} />
          <Route path="/subscriptions/" component={SubscriptionList} />
          <Route path="/cards/" component={CardList} exact />
          <Route path="/cards/:cardid" component={CardWithTransactions} exact />
        </Suspense>
      </div>
    </Router>
  );
};

const Index: React.FC = () => <div>Welcome!</div>;

export default App;
