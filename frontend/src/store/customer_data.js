import axios from "axios";
const customer = {
  namespaced: true,
  state: {
    customer: null,
    data_post: {
      id: null,
      customer_id: null,
      class_id: null,
    },
  },

  mutations: {
    set_value: (state, value) => {
      state.customer = value;
    },
  },
  getters: {
    getbyid: (state) => (id) => {
      return state.customer.filter((e) => e.id == id);
    },
  },
  actions: {
    async get_all({ commit }) {
      const data = await axios.get("http://localhost:3000/customer");
      commit("set_value", data.data);
    },
    async add_cus({ commit, state }) {
      console.log(commit);
      await axios.post("http://localhost:3000/customer_class", state.data_post);
    },
  },
};
export default customer;
