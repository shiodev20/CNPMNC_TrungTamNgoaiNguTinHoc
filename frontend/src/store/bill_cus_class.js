import axios from "axios";
const bill_cus_classs = {
  namespaced: true,
  state: {
    bill_cus_class: null,
    status_chose: 1,
  },
  mutations: {
    set_value: (state, value) => {
      state.bill_cus_class = value;
    },
  },
  getters: {
    getbyid: (state) => (idcus, idclass) => {
      return state.cus_class.filter(
        (e) => e.customer_id == idcus && e.class_id == idclass
      )[0];
    },
    getbyids: (state) => (idcus, idclass) => {
      return state.bill_cus_class.filter(
        (e) => e.id_cus == idcus && e.id_class == idclass
      );
    },
  },
  actions: {
    async add_cus({ commit }, data) {
      console.log(commit);
      await axios.post("http://localhost:3000/bill_cus_class", data);
    },
    async get_all({ commit }) {
      const data = await axios.get("http://localhost:3000/bill_cus_class");
      commit("set_value", data.data);
    },
  },
};
export default bill_cus_classs;
